using Gameplay.Enemies;
using Gameplay.Enemies.Ball;
using Gameplay.Projectile;
using PlatformerRails;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class KlonoaBehaviour : MonoBehaviour
    {
        [SerializeField] private KlonoaDefinition _definition;
        [Space]
        [SerializeField] private float _maxGroundDistance = 0.5f;
        [SerializeField] private float _groundCheckLength = 0.05f;
        [SerializeField] private LayerMask _groundLayer;
        [Space]
        [SerializeField] private float _minWalkSpeed = 0.1f;
        [Space]
        [SerializeField] private Transform _captureProjectileOrigin;
        [SerializeField] private Transform _EnemyProjectileOrigin;
        [Space]
        [SerializeField] private Transform _ballHolder;

        private bool _jumpKeep;
        private bool _jumpActivated;
        private float _floatYSpeed;
        private bool _floatUsed;

        MoverOnRails _mover;
        Rigidbody _rigidbody;
        KlonoaState _currentState;
        KlonoaState _normalState;
        KlonoaState _floatState;
        KlonoaState _captureState;
        KlonoaState _holdingState;

        CaptureProjectile _projectile;
        EnemyBall _holdedBall;
        CollisionData _collisionData;

        public Vector2 MoveDirection { set; private get; }
        public bool Grounded => _collisionData.Grounded;
        public bool Walking => Mathf.Abs(EffectiveSpeed.z) > _minWalkSpeed && Mathf.Abs(MoveDirection.x) > 0;
        public bool Floating => _currentState == _floatState;
        public Vector3 EffectiveSpeed => _mover.Velocity;
        public float Facing { get; private set; } = 1;

        //Events
        public event Action CaptureProjectileEvent;
        public event Action BeginHoldingEvent;
        public event Action EndHoldingEvent;
        public event Action ThrowEnemyEvent;

        //Behaviour Methods
        void Awake()
        {
            _mover = GetComponent<MoverOnRails>();
            _rigidbody = GetComponent<Rigidbody>();
            _collisionData = new CollisionData(_maxGroundDistance, _groundCheckLength, _groundLayer);
            _normalState = new KlonoaState(
                moveSpeed: _definition.MoveSpeed, gravity: _definition.Gravity, canTurn: true,
                jumpAction: StartJumpAction,
                jumpKeepAction: FloatAction,
                attackAction: StartCapture);
            _floatState = new KlonoaState(
                moveSpeed: _definition.FloatMoveSpeed, canTurn: true, exitTime: _definition.FloatTime,
                passiveAction: FloatUpdate,
                exitAction: ChangeToNormal,
                jumpReleaseAction: ChangeToNormal);
            _captureState = new KlonoaState(
                moveSpeed: _definition.NotMoveSpeed, gravity: _definition.Gravity, canTurn: false);
            _holdingState = new KlonoaState(
                moveSpeed: _definition.MoveSpeed, gravity: _definition.Gravity, canTurn: true,
                jumpAction: StartJumpAction,
                attackAction: ThrowHoldedEnemy);

            ChangeState(_normalState);
        }

        void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            Vector3 velocity = _mover.Velocity;
            //To make X value 0 means locate the character just above the rail
            velocity.x = -_mover.Position.x * 5f;
            _mover.Velocity = velocity;
            CheckGroundDistance();

            _currentState.FixedUpdate(_mover, MoveDirection, _collisionData, deltaTime);
            if (_jumpKeep)
                _currentState.JumpKeepAction();

            JumpAction(deltaTime);
            UpdateFacing();
            _jumpActivated = false;
        }

        void CheckGroundDistance()
        {
            bool previousGrounded = _collisionData.Grounded;
            _collisionData.CheckGround(transform);

            if (_collisionData.Grounded && !previousGrounded) 
            {
                _floatUsed = false;
            }
        }

        private void JumpAction(float deltaTime)
        {
            if (Grounded && _jumpActivated)
            {
                Vector3 velocity = _mover.Velocity;
                velocity.y = _definition.JumpSpeed + Mathf.Max(0, (_maxGroundDistance - _collisionData.GroundDistance) / deltaTime);
                _mover.Velocity = velocity;
            }
        }

        private void UpdateFacing()
        {
            if (Walking) Facing = Mathf.Sign(_mover.Velocity.z);
        }

        //Input Access Methods
        public void StartJump()  
        {
            _currentState.JumpAction();
            _jumpKeep = true;
        }

        public void EndJump()
        {
            _jumpKeep = false;
            _currentState.JumpReleaseAction();
        }

        public void StartAttack()
        {
            _currentState.AttackAction();
        }

        public void StopAttack() 
        {
            //unused, added for completition
        }

        //States Actions
        private void StartJumpAction()
        {
            _jumpActivated = true;
        }

        private void FloatAction()
        {
            if (!_floatUsed && !Grounded && _mover.Velocity.y < 0)
            {
                _floatUsed = true;
                _floatYSpeed = _definition.FloatStartSpeed;
                ChangeState(_floatState);
            }
        }

        private void FloatUpdate(float deltaTime)
        {
            Vector3 velocity = _mover.Velocity;
            _floatYSpeed += _definition.FloatAcceleration * deltaTime;
            velocity.y = _floatYSpeed;
            _mover.Velocity = velocity;
        }

        private void StartCapture()
        {
            if (_projectile != null) return;
            _projectile = Instantiate(_definition.CaptureProjectile, _captureProjectileOrigin.position, Quaternion.identity);
            _projectile.MovingFinishEvent += OnCaptureEventFinish;
            _projectile.ReturnFinishEvent += OnReturnEventFinish;
            _projectile.EnemyCapturedEvent += OnEnemyCaptured;
            Debug.Log("Facing: " + Facing);
            _projectile.StartMovement(transform.forward * Facing, _mover.Velocity.z, _captureProjectileOrigin);
            CaptureProjectileEvent?.Invoke();
            ChangeState(_captureState);
        }

        private void OnCaptureEventFinish()
        {
            if (!Grounded)
            {
                FinishCapture();
            }
        }

        private void OnReturnEventFinish()
        {
            FinishCapture();
        }

        private void FinishCapture()
        {
            _projectile.MovingFinishEvent -= OnCaptureEventFinish;
            _projectile.ReturnFinishEvent -= OnReturnEventFinish;
            _projectile.EnemyCapturedEvent -= OnEnemyCaptured;
            ChangeState(_normalState);
        }

        private void OnEnemyCaptured(EnemyBehaviour enemy)
        {
            _projectile.MovingFinishEvent -= OnCaptureEventFinish;
            _projectile.ReturnFinishEvent -= OnReturnEventFinish;
            _projectile.EnemyCapturedEvent -= OnEnemyCaptured;

            _holdedBall = enemy.InstantiateBall(_ballHolder, _rigidbody);
            _holdedBall.DestroyEvent += OnEndHolding;
            BeginHoldingEvent?.Invoke();
            ChangeState(_holdingState);
        }

        private void ThrowHoldedEnemy()
        {
            _holdedBall.transform.position = _EnemyProjectileOrigin.transform.position;
            _holdedBall.Throw(Facing);
            _holdedBall = null;
            ThrowEnemyEvent?.Invoke();
            ChangeToNormal();
        }

        private void OnEndHolding()
        {
            _holdedBall = null;
            EndHoldingEvent?.Invoke();
            ChangeToNormal();
        }

        private void ChangeToNormal()
        {
            ChangeState(_normalState);
        }

        private void ChangeState(KlonoaState newState)
        {
            _currentState = newState;
            _currentState.Restart();
        }


#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Vector3.zero, Vector3.down * (_maxGroundDistance + _groundCheckLength));
            Gizmos.DrawWireCube(Vector3.down * _maxGroundDistance, Vector3.right / 2 + Vector3.forward / 2);
            Gizmos.matrix = Matrix4x4.identity;
        }
#endif
    }
}

