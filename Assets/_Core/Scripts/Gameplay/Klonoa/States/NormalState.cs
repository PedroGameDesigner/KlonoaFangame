using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class NormalState : KlonoaState
    {
        private const bool CAN_TURN = true;

        private KlonoaState _floatState;
        private KlonoaState _captureState;

        private bool _floatUsed;
        private bool _previousGrounded;

        protected override SpeedData MoveSpeed => _definition.MoveSpeed;
        protected override float Gravity => _definition.Gravity;
        protected override bool CanTurn => CAN_TURN;

        public NormalState(KlonoaBehaviour behaviour) : base(behaviour) { }
        
        public void SetStates(KlonoaState floatState, KlonoaState captureState)
        {
            _floatState = floatState;
            _captureState = captureState;
        }

        public override void FixedUpdate(float deltaTime)
        {
            base.FixedUpdate(deltaTime);

            CheckGrounded();
        }

        void CheckGrounded()
        {
            if (_behaviour.CollisionData.Grounded && !_previousGrounded)
            {
                _floatUsed = false;
            }

            _previousGrounded = _behaviour.CollisionData.Grounded;
        }

        public override void JumpAction()
        {
            _behaviour.StartJumpAction();
        }

        public override void JumpKeepAction()
        {
            if (!_floatUsed && !_behaviour.Grounded && _mover.Velocity.y < 0)
            {
                _floatUsed = true;
                ChangeState(_floatState);
            }
        }

        public override void AttackAction()
        {
            if (!_behaviour.CaptureProjectileThrowed)
                ChangeState(_captureState);
        }
        
        public override void JumpReleaseAction() { /* UNUSED */ }
    }
}
