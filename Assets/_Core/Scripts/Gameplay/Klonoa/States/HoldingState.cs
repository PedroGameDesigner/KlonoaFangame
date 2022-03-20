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

        protected override SpeedData MoveSpeed => _definition.MoveSpeed;
        protected override float Gravity => _definition.Gravity;
        protected override bool CanTurn => CAN_TURN;

        public HoldingState(KlonoaBehaviour behaviour) : base(behaviour) { }

        public void SetStates(KlonoaState normalState)
        {
            _normalState = normalState;
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
            if (_behaviour.Grounded)
            {
                _behaviour.StartJumpAction();
            }
        }

        public override void AttackAction()
        {
            _behaviour.ThrowHoldedEnemy();
            ChangeState(_normalState);
        }

        public override void JumpKeepAction() { }

        public override void JumpReleaseAction() { }
    }
}