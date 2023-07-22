using Colliders;
using Gameplay.Klonoa;
using PlatformerRails;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class MooBehaviour : EnemyBehaviour
    {
        [SerializeField] private float _rightStopDistance;
        [SerializeField] private float _leftStopDistance;
        [SerializeField] private float _gravity;
        [SerializeField] private float _terminalVelocity;
        [SerializeField] private Direction _startDirection = Direction.Left;
        [SerializeField] private CharacterOnRails _mover;
        [Space]
        [SerializeField] private RailBehaviour _DebugRail;

        private MooState _mooState;

        private float _firstStopDistance;
        private Direction _walkDirection;
        private Vector3 _lastPosition;
        private float _distanceToWalk;

        private float _stopTimer;

        public override bool CanBeCaptured => true;
        public int Facing => (int) _walkDirection;
        public bool IsWalking => IsActive && _mooState == MooState.Move;
        public float Speed => _definition.MoveSpeed;
        private float StopDistance => Mathf.Abs(_rightStopDistance) + Mathf.Abs(_leftStopDistance);
        private Vector3 Velocity => Speed * Vector3.forward * (int) _walkDirection;

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        private void Initialize()
        {
            _mooState = MooState.Move;
            _walkDirection = _startDirection;
            _firstStopDistance = _walkDirection == Direction.Left ?
                _leftStopDistance : _rightStopDistance;
            _distanceToWalk = _firstStopDistance;
        }

        private void Start()
        {
            _lastPosition = _mover.Rail.World2Local(transform.position).Value;
            _mover.UpdateLocalPosition();
        }

        protected override void UpdateActiveState(float deltaTime)
        {
            switch (_mooState)
            {
                case MooState.Move:
                    UpdateMoveState(deltaTime);
                    break;
                case MooState.Stop: UpdateStopState(deltaTime); break;
            }
        }

        private void UpdateMoveState(float deltaTime)
        {
            UpdateMove(deltaTime);
            UpdateGravity(deltaTime);
            CalculateTranslation(deltaTime);
            _lastPosition = _mover.Position;
        }

        private void UpdateMove(float deltaTime)
        {
            Vector3 velocity = _mover.Velocity;
            velocity.z = Velocity.z;
            _mover.Velocity = velocity;  
        }

        protected void UpdateGravity(float deltaTime)
        {
            if (_gravity <= 0) return;
            //Y+ axis = Upwoard (depends on rail rotation)
            Vector3 velocity = _mover.Velocity;
            velocity.y -= _gravity * deltaTime;

            if (velocity.y < -_terminalVelocity)
                velocity.y = -_terminalVelocity;

            _mover.Velocity = velocity;
        }

        protected void CalculateTranslation(float deltaTime)
        {
            float translation = Mathf.Abs(_lastPosition.z - _mover.Position.z);
            _distanceToWalk -= translation;

            if (_distanceToWalk <= 0)
            {
                ChangeToStop();
            }
        }

        private void UpdateStopState(float deltaTime)
        {
            _mover.Velocity = Vector3.zero;
            _stopTimer += deltaTime;

            if (_stopTimer >= _definition.StopTime)
            {
                ChangeToMove();
            }
        }
        protected override void ChangeToActiveState()
        {
            _mover.enabled = true;
            Initialize();
            base.ChangeToActiveState();
        }

        private void ChangeToMove()
        {
            _walkDirection = _walkDirection == Direction.Rigth ?
                Direction.Left : Direction.Rigth;
            _distanceToWalk = StopDistance;
            _mooState = MooState.Move;
            InvokeStateChangeEvent();
            _lastPosition = _mover.Position;
        }

        private void ChangeToStop()
        {
            _stopTimer = 0;
            _mooState = MooState.Stop;
            InvokeStateChangeEvent();
        }

        protected override void Kill()
        {
            _mover.enabled = false;

            base.Kill();
        }

        private void OnDisable()
        {
            _mover.Velocity = Vector3.zero;
        }

        private void OnDrawGizmosSelected()
        {
            if (_DebugRail != null && !Application.isPlaying)
            {
                Vector3 railPos = _DebugRail.World2Local(transform.position).Value;

                Vector3 rightPoint = _DebugRail.Local2World(railPos + Vector3.forward * _rightStopDistance);
                Vector3 leftPoint = _DebugRail.Local2World(railPos - Vector3.forward * _leftStopDistance);

                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(rightPoint, 0.35f);

                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(leftPoint, 0.35f);
            }
        }

        private enum MooState
        {
            Move,
            Stop
        }

        private enum Direction
        {
            Rigth = 1,
            Left = -1
        }
    }
}
