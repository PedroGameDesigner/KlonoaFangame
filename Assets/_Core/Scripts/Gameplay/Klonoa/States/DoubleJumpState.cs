using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class DoubleJumpState : KlonoaState
    {
        private KlonoaState _normalState;
        private bool _ballInPosition;

        public override bool IsNormalState => false;

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
            _mover.Velocity = Vector3.zero;
            _mover.enabled = false;
            _behaviour.MoveBeforeDoubleJump(OnDoubleJumpPosition);
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

        private void OnDoubleJumpPosition()
        {
            _mover.enabled = true;
            _behaviour.ThrowHoldedEnemyDownwards();
            _behaviour.StartJumpAction(_definition.DoubleJumpSpeed, ignoreGround: true, invokeJumpEvent: false, allowFloat: true);
            _ballInPosition = true;
        }

        public override void AttackAction() { }
        public override void JumpAction() { }
        public override void JumpKeepAction() { }
        public override void JumpReleaseAction() { }
    }
}