using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class NormalState : KlonoaState
    {
        private const bool CAN_TURN = true;
        private const float STUCK_STATE_TIME = 0.1f;

        private KlonoaState _floatState;
        private KlonoaState _captureState;

        private bool _floatUsed;
        private bool _previousGrounded;
        private float _lastYVelocity;
        private float _timer;

        public override bool IsNormalState => true;

        protected override SpeedData MoveSpeed => _definition.MoveSpeed;
        protected override float Gravity => _definition.Gravity;
        protected override bool CanTurn => CAN_TURN;
        protected bool CanChangeState => _timer >= STUCK_STATE_TIME;

        public NormalState(KlonoaBehaviour behaviour) : base(behaviour) { }
        
        public void SetStates(KlonoaState floatState, KlonoaState captureState)
        {
            _floatState = floatState;
            _captureState = captureState;
        }

        public override void Enter()
        {
            base.Enter();
            _timer = 0;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            _timer += deltaTime;
        }

        public override void FixedUpdate(float deltaTime)
        {
            base.FixedUpdate(deltaTime);

            CheckGrounded();
        }

        void CheckGrounded()
        {
            if (_behaviour.IsGrounded && !_previousGrounded)
            {
                _floatUsed = false;
            }

            _previousGrounded = _behaviour.IsGrounded;
            _lastYVelocity = _mover.Velocity.y;
        }

        public override void Reset()
        {
            _timer = 0;
            _floatUsed = false;
        }

        public override void JumpAction()
        {
            _behaviour.StartJumpAction(_definition.JumpSpeed);
        }

        public override void JumpKeepAction()
        {
            if (CanChangeState && !_floatUsed && _mover.Velocity.y < 0 && (!_behaviour.CanJump() || _behaviour.AllowFloat))
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
