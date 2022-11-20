using Gameplay.Enemies.Ball;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.Klonoa
{
    public class HoldingState : KlonoaState
    {
        private const bool CAN_TURN = true;

        protected EnemyBall _holdedBall;
        protected KlonoaState _normalState;
        protected KlonoaState _doubleJumpState;

        public override bool IsNormalState => true;

        protected override SpeedData MoveSpeed => _definition.MoveSpeed;
        protected override float Gravity => _definition.Gravity;
        protected override bool CanTurn => CAN_TURN;

        public HoldingState(KlonoaBehaviour behaviour) : base(behaviour) { }

        public void SetStates(KlonoaState normalState, KlonoaState doubleJumpState)
        {
            _normalState = normalState;
            _doubleJumpState = doubleJumpState;
        }

        public override void Enter()
        {
            base.Enter();
            _holdedBall = _behaviour.HoldedBall;
            _holdedBall.DestroyEvent += OnEndHolding;
            _behaviour.InvokeBeginHoldingEvent();
        }

        private void OnEndHolding()
        {
            _behaviour.InvokeEndHoldingEvent();
            ChangeState(_normalState);
        }

        public override void JumpAction()
        {
            if (_behaviour.IsGrounded)
                _behaviour.StartJumpAction(_definition.JumpSpeed);
            else
            {
                _behaviour.InvokeEndHoldingEvent();
                ChangeState(_doubleJumpState);
            }
        }

        public override void AttackAction()
        {
            _behaviour.ThrowHoldedEnemySideways();
            OnEndHolding();
        }

        public override void Exit()
        {
            base.Exit();
            if (_holdedBall != null)
                _holdedBall.DestroyEvent -= OnEndHolding;
        }

        public override void JumpKeepAction() { }

        public override void JumpReleaseAction() { }
    }
}