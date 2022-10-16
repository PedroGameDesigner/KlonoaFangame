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

        private State _state;

        private float _firstStopDistance;
        private Direction _walkDirection;
        private float _distanceToWalk;

        private float _stopTimer;

        public override bool CanBeCaptured => true;
        public int Facing => (int) _walkDirection;
        public bool IsWalking => _state == State.Move;
        public float Speed => _definition.MoveSpeed;
        private float StopDistance => Mathf.Abs(_rightStopDistance) + Mathf.Abs(_leftStopDistance);
        private Vector3 Velocity => Speed * Vector3.forward * (int) _walkDirection;

        public event Action StateChangeEvent;

        private void Start()
        {
            _state = State.Move;
            _walkDirection = _startDirection;
            _firstStopDistance = _walkDirection == Direction.Left ?
                _leftStopDistance : _rightStopDistance;
            _distanceToWalk = _firstStopDistance;
        }

        private void Update()
        {            
            float deltaTime = Time.deltaTime;
            switch (_state)
            {
                case State.Move: UpdateMove(deltaTime); return;
                case State.Stop: UpdateStop(deltaTime); return;
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

        private void ChangeToMove()
        {
            _walkDirection = _walkDirection == Direction.Rigth ?
                Direction.Left : Direction.Rigth;
            _distanceToWalk = StopDistance;
            _state = State.Move;
            StateChangeEvent?.Invoke();
        }

        private void ChangeToStop()
        {
            _stopTimer = 0;
            _state = State.Stop;
            StateChangeEvent?.Invoke();
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

        private enum State
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
