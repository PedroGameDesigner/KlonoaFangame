using PlatformerRails;
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
        [Space]
        [SerializeField] private float _minWalkSpeed = 0.1f;

        public bool _jumpKeep;
        private bool _jumpActivated;
        private float? _groundDistance;
        private float _floatYSpeed;
        private bool _floatUsed;

        MoverOnRails _mover;
        KlonoaState _currentState;
        KlonoaState _normalState;
        KlonoaState _floatState;

        public Vector2 MoveDirection { set; private get; }
        public bool Grounded => _groundDistance != null;
        public bool Walking => Mathf.Abs(EffectiveSpeed.z) > _minWalkSpeed && Mathf.Abs(MoveDirection.x) > 0;
        public Vector3 EffectiveSpeed => _mover.Velocity;
        public float Facing { get; private set; }

        //Behaviour Methods
        void Awake()
        {
            _mover = GetComponent<MoverOnRails>();
            _normalState = new KlonoaState(
                moveSpeed: _definition.MoveSpeed, groundDistance: _maxGroundDistance, gravity: _definition.Gravity, canTurn: true,
                jumpAction: StartJumpAction,
                jumpKeepAction: FloatAction);
            _floatState = new KlonoaState(
                moveSpeed: _definition.FloatMoveSpeed, canTurn: true, exitTime: _definition.FloatTime,
                passiveAction: FloatUpdate,
                exitAction: ChangeToNormal,
                jumpReleaseAction: ChangeToNormal);

            ChangeState(_normalState);
        }

        void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            //To make X value 0 means locate the character just above the rail
            _mover.Velocity.x = -_mover.Position.x * 5f;
            _groundDistance = CheckGroundDistance();

            _currentState.FixedUpdate(_mover, MoveDirection, deltaTime, _groundDistance);
            if (_jumpKeep)
                _currentState.JumpKeepAction();

            JumpAction(deltaTime);
            UpdateFacing();
            _jumpActivated = false;
        }

        float? CheckGroundDistance()
        {
            RaycastHit info;
            var hit = Physics.Raycast(transform.position, -transform.up, out info, _maxGroundDistance + _groundCheckLength);
            if (hit) 
            {
                _floatUsed = false;
                return info.distance;
            } else
                return null;
        }

        private void JumpAction(float deltaTime)
        {
            if (Grounded && _jumpActivated)
                _mover.Velocity.y = _definition.JumpSpeed + Mathf.Max(0, (_maxGroundDistance - _groundDistance.Value) / deltaTime);
        }

        private void UpdateFacing()
        {
            if (Walking) Facing = _mover.Velocity.z;
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
            _floatYSpeed += _definition.FloatAcceleration * deltaTime;
            _mover.Velocity.y = _floatYSpeed;
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

