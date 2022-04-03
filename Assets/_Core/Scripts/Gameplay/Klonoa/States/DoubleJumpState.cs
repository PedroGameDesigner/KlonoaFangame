using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class DoubleJumpState : KlonoaState
    {
        private KlonoaState _normalState;
        private bool _ballInPosition;

        protected override SpeedData MoveSpeed => _definition.NotMoveSpeed;
        protected override float Gravity => 0;
        protected override bool CanTurn => false;

        public DoubleJumpState(KlonoaBehaviour behaviour) : base(behaviour) { }

        public void SetStates(KlonoaState normalState)
        {
            _normalState = normalState;
        }

        public override void Enter()
        {
            base.Enter();
            _behaviour.MoveBallToFeet(OnBallInPosition);
            _ballInPosition = false;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (_ballInPosition)
            {
                ChangeState(_normalState);
            }
        }

        private void OnBallInPosition()
        {
            _behaviour.ThrowHoldedEnemyDownwards();
            _behaviour.StartJumpAction(_definition.DoubleJumpSpeed, true);
            _ballInPosition = true;
        }

        public override void AttackAction() { }
        public override void JumpAction() { }
        public override void JumpKeepAction() { }
        public override void JumpReleaseAction() { }
    }
}