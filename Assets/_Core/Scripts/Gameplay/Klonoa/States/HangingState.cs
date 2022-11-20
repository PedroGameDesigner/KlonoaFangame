using Gameplay.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.Klonoa
{
    public class HangingState : KlonoaState
    {
        private const bool CAN_TURN = true;

        private KlonoaState _doubleJumpState;
        private bool _inPosition;

        public override bool IsNormalState => false;

        protected override SpeedData MoveSpeed => _definition.NotMoveSpeed;
        protected override float Gravity => 0;
        protected override bool CanTurn => CAN_TURN;
        private HangeableObject HangingObject => _behaviour.HangingObject;


        public HangingState(KlonoaBehaviour behaviour) : base(behaviour) { }

        public void SetStates(KlonoaState doubleJumpState)
        {
            _doubleJumpState = doubleJumpState;
        }

        public override void Enter()
        {
            base.Enter();
            _mover.Velocity = Vector3.zero;
            _mover.enabled = false;
            _inPosition = false;
            HangingObject.MoveToHangingPosition(_definition.HangingRepositionTime, OnHangingFinished);
        }

        private void OnHangingFinished()
        {
            _inPosition = true;
        }

        public override void JumpAction() 
        {
            if (_inPosition)
            {
                ChangeState(_doubleJumpState);
            }
        }

        public override void AttackAction() { /* do nothing */ }

        public override void JumpKeepAction() { /* do nothing */ }

        public override void JumpReleaseAction() { /* do nothing */ }
    }
}
