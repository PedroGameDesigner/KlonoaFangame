using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class FloatState : KlonoaState
    {
        private const bool CAN_TURN = true;

        private KlonoaState _normalState;
        private float _YSpeed;
        private float _timer;

        protected override SpeedData MoveSpeed => _definition.FloatMoveSpeed;
        protected override float Gravity => 0;
        protected override bool CanTurn => CAN_TURN;

        public FloatState(KlonoaBehaviour behaviour) : base(behaviour) { }

        public void SetStates(KlonoaState normalState)
        {
            _normalState = normalState;
        }

        public override void Enter()
        {
            base.Enter();
            _YSpeed = _definition.FloatStartSpeed;
            _timer = 0;
        }

        public override void FixedUpdate(float deltaTime)
        {
            base.FixedUpdate(deltaTime);

            FloatUpdate(deltaTime);
            TimerUpdate(deltaTime);
        }

        private void FloatUpdate(float deltaTime)
        {
            Vector3 velocity = _mover.Velocity;
            _YSpeed += _definition.FloatAcceleration * deltaTime;
            velocity.y = _YSpeed;
            _mover.Velocity = velocity;
        }

        private void TimerUpdate(float deltaTime)
        {
            _timer += deltaTime;

            if (_timer >= _definition.FloatTime)
            {
                EndState();
            }
        }

        public override void JumpReleaseAction() 
        {
            EndState();
        }

        private void EndState()
        {
            ChangeState(_normalState);
        }

        public override void AttackAction() { }
        public override void JumpAction() { }
        public override void JumpKeepAction() { }

    }
}
