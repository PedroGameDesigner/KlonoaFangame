using Gameplay.Enemies;
using Gameplay.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class CaptureState : KlonoaState
    {
        private const bool CANT_TURN = false;

        protected KlonoaState _normalState;
        protected KlonoaState _holdingState;
        protected KlonoaState _hangingState;
        protected CaptureProjectile _projectile;

        protected override SpeedData MoveSpeed => _definition.NotMoveSpeed;
        protected override float Gravity => _definition.Gravity;
        protected override bool CanTurn => CANT_TURN;

        public CaptureState(KlonoaBehaviour behaviour) : base(behaviour) { }

        public void SetStates(KlonoaState normalState, KlonoaState holdingState, KlonoaState hangingState)
        {
            _normalState = normalState;
            _holdingState = holdingState;
            _hangingState = hangingState;
        }

        public override void Enter()
        {
            base.Enter();
            _projectile = _behaviour.InstantiateCapture();
            _projectile.MovingFinishEvent += OnCaptureEventFinish;
            _projectile.ReturnFinishEvent += OnReturnEventFinish;
            _projectile.CapturedEvent += OnObjectCaptured;
        }

        private void OnCaptureEventFinish()
        {
            if (!_behaviour.IsGrounded)
            {
                FinishCapture();
            }
        }

        private void OnReturnEventFinish()
        {
            FinishCapture();
        }

        private void FinishCapture()
        {
            UnsubscribeEvents();
            ChangeState(_normalState);
        }

        private void OnObjectCaptured(ICapturable capturedObject)
        {
            UnsubscribeEvents();
            switch (capturedObject)
            {
                case HoldableObject holdableObject:
                    _behaviour.HoldObject(holdableObject);
                    ChangeState(_holdingState);
                    break;
                case HangeableObject hangeableObject:
                    _behaviour.HangFromObject(hangeableObject);
                    ChangeState(_hangingState);
                    break;
            }            
        }

        private void UnsubscribeEvents()
        {
            _projectile.MovingFinishEvent -= OnCaptureEventFinish;
            _projectile.ReturnFinishEvent -= OnReturnEventFinish;
            _projectile.CapturedEvent -= OnObjectCaptured;
        }

        public override void AttackAction() { }

        public override void JumpAction() { }

        public override void JumpKeepAction() { }

        public override void JumpReleaseAction() { }
    }
}
