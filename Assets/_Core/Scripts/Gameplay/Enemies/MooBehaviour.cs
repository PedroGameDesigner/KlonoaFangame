using PlatformerRails;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class MooBehaviour : EnemyBehaviour
    {
        [SerializeField] private float _rightStopDistance;
        [SerializeField] private float _leftStopDistance;
        [SerializeField] private Direction _startDirection = Direction.Left;
        [SerializeField] private MoverOnRails _mover;
        [Space]
        [SerializeField] private RailBehaviour _DebugRail;

        private MooState _mooState;

        private float _firstStopDistance;
        private Direction _walkDirection;
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

        protected override void UpdateActiveState(float deltaTime)
        {            
            switch (_mooState)
            {
                case MooState.Move: UpdateMove(deltaTime); return;
                case MooState.Stop: UpdateStop(deltaTime); return;
            }
        }

        private void UpdateMove(float deltaTime)
        {
            _mover.Velocity = Velocity;
            float translation = _mover.Velocity.magnitude * deltaTime;
            _distanceToWalk -= translation;

            if (_distanceToWalk <= 0)
            {
                ChangeToStop();
            }
        }

        private void UpdateStop(float deltaTime)
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
            base.ChangeToActiveState();
            _mover.enabled = true;
            Initialize();
        }

        private void ChangeToMove()
        {
            _walkDirection = _walkDirection == Direction.Rigth ?
                Direction.Left : Direction.Rigth;
            _distanceToWalk = StopDistance;
            _mooState = MooState.Move;
            InvokeStateChangeEvent();
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
