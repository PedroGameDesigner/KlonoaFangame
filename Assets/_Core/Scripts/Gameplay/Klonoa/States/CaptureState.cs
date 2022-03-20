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
        protected CaptureProjectile _projectile;

        protected override SpeedData MoveSpeed => _definition.NotMoveSpeed;
        protected override float Gravity => _definition.Gravity;
        protected override bool CanTurn => CANT_TURN;

        public CaptureState(KlonoaBehaviour behaviour) : base(behaviour) { }

        public void SetStates(KlonoaState normalState, KlonoaState holdingState)
        {
            _normalState = normalState;
            _holdingState = holdingState;
        }

        public override void Enter()
        {
            base.Enter();
            _projectile = _behaviour.InstantiateCapture();
            _projectile.MovingFinishEvent += OnCaptureEventFinish;
            _projectile.ReturnFinishEvent += OnReturnEventFinish;
            _projectile.EnemyCapturedEvent += OnEnemyCaptured;
        }

        private void OnCaptureEventFinish()
        {
            if (!_behaviour.Grounded)
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

        private void OnEnemyCaptured(EnemyBehaviour enemy)
        {
            UnsubscribeEvents();
            _behaviour.HoldEnemy(enemy);
            ChangeState(_holdingState);
        }

        private void UnsubscribeEvents()
        {
            _projectile.MovingFinishEvent -= OnCaptureEventFinish;
            _projectile.ReturnFinishEvent -= OnReturnEventFinish;
            _projectile.EnemyCapturedEvent -= OnEnemyCaptured;
        }

        public override void AttackAction() { }

        public override void JumpAction() { }

        public override void JumpKeepAction() { }

        public override void JumpReleaseAction() { }
    }
}
