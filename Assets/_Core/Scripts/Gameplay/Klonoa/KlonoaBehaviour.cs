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
        [SerializeField] private Transform _enemyProjectileOrigin;
        [SerializeField] private Transform _feetPosition;
        [Space]
        [SerializeField] private BallHolder _ballHolder;

        private bool _jumpKeep;
        private bool _jumpActivated;
        private bool _ignoreGround;
        private float _jumpForce;
        private float _floatYSpeed;

        KlonoaStateMachine _stateMachine;
        MoverOnRails _mover;
        Rigidbody _rigidbody;

        CaptureProjectile _projectile;

        public KlonoaDefinition Definition => _definition;
        private Vector2 MoveDirection { set;  get; }
        public CollisionData CollisionData { get; private set; } 
        public bool Grounded => CollisionData.Grounded;
        public bool Walking => Mathf.Abs(EffectiveSpeed.z) > _minWalkSpeed && Mathf.Abs(MoveDirection.x) > 0;
        public bool Floating => _stateMachine.IsFloatState;
        public bool CaptureProjectileThrowed => _projectile != null;
        public Vector3 EffectiveSpeed => _mover.Velocity;
        public float Facing { get; private set; } = 1;
        public EnemyBall HoldedBall { get; private set; }

        //Events
        public event Action CaptureProjectileEvent;
        public event Action BeginHoldingEvent;
        public event Action EndHoldingEvent;
        public event Action ThrowEnemyEvent;

        public event Action JumpEvent;
        public event Action JumpKeepEvent;
        public event Action JumpReleaseEvent;
        public event Action AttackEvent;
        public event Action<Vector2> DirectionChangeEvent;

        //Behaviour Methods
        void Awake()
        {
            _stateMachine = new KlonoaStateMachine(this);
            _stateMachine.StartMachine();
            _mover = GetComponent<MoverOnRails>();
            _rigidbody = GetComponent<Rigidbody>();
            CollisionData = new CollisionData(_maxGroundDistance, _groundCheckLength, _groundLayer);
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            Vector3 velocity = _mover.Velocity;
            //To make X value 0 means locate the character just above the rail
            velocity.x = -_mover.Position.x * 5f;
            _mover.Velocity = velocity;
            CollisionData.CheckGround(transform);

            _stateMachine.FixedUpdate(deltaTime);
            if (_jumpKeep)
                JumpKeepEvent?.Invoke();

            JumpAction(deltaTime);
            UpdateFacing();
            _jumpActivated = false;
        }

        private void JumpAction(float deltaTime)
        {
            if ((Grounded || _ignoreGround ) && _jumpActivated)
            {
                Vector3 velocity = _mover.Velocity;
                velocity.y = _jumpForce + Mathf.Max(0, (_maxGroundDistance - CollisionData.GroundDistance) / deltaTime);
                Debug.Log("Klonoa: JUMP FORCE: " + velocity.y);
                _mover.Velocity = velocity;
            }
        }

        private void UpdateFacing()
        {
            if (Walking) Facing = Mathf.Sign(_mover.Velocity.z);
        }
        
        private void LateUpdate()
        {
            _stateMachine.LateUpdate(Time.deltaTime);
        }

        //Input Access Methods
        public void SetMoveDirection(Vector2 direction)
        {
            MoveDirection = direction;
            DirectionChangeEvent?.Invoke(MoveDirection);
        }

        public void StartJump()  
        {
            JumpEvent?.Invoke();
            _jumpKeep = true;
        }

        public void EndJump()
        {
            _jumpKeep = false;
            JumpReleaseEvent?.Invoke();
        }

        public void StartAttack()
        {
            AttackEvent?.Invoke();
        }

        public void StopAttack() 
        {
            //unused, added for completition
        }

        //States Actions
        public void StartJumpAction(float jumpForce, bool ignoreGround = false)
        {
            _jumpActivated = true;
            _jumpForce = jumpForce;
            _ignoreGround = ignoreGround;
        }        

        public CaptureProjectile InstantiateCapture()
        {
            _projectile = Instantiate(_definition.CaptureProjectile, _captureProjectileOrigin.position, Quaternion.identity);
            _projectile.StartMovement(transform.forward * Facing, _mover.Velocity.z, _captureProjectileOrigin);
            CaptureProjectileEvent?.Invoke();

            return _projectile;
        }
        
        public void HoldEnemy(EnemyBehaviour enemy)
        {
            HoldedBall = enemy.InstantiateBall(_ballHolder.transform, _rigidbody);
            _ballHolder.SetHoldedBall(HoldedBall);
            _ballHolder.RestoreOriginPosition();
            _ballHolder.MoveFromPosition(enemy.transform.position, _definition.CaptureRepositionTime);
        }

        public void MoveBallToFeet(Action finishAction)
        {
            _ballHolder.MoveToPosition(
                _feetPosition.position,
                _definition.DoubleJumpPreparationTime,
                finishAction);
        }

        public void ThrowHoldedEnemySideways()
        {
            if (HoldedBall == null) return;

            HoldedBall.transform.position = _enemyProjectileOrigin.position;
            HoldedBall.ThrowSide(Facing);
            HoldedBall = null;
            ThrowEnemyEvent?.Invoke();
        }

        public void ThrowHoldedEnemyDownwards()
        {
            if (HoldedBall == null) return;

            HoldedBall.ThrowDown();
            HoldedBall = null;
            ThrowEnemyEvent?.Invoke();
        }

        public void InvokeBeginHoldingEvent()
        {
            BeginHoldingEvent?.Invoke();
        }

        public void InvokeEndHoldingEvent()
        {
            EndHoldingEvent?.Invoke();
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

