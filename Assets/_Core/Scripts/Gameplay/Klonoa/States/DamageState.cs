using PlatformerRails;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class DamageState : KlonoaState
    {
        private const bool CANT_TURN = false;
        
        private float _timer;
        private RaycastHit _hit;
        private KlonoaState _nextState;

        protected override SpeedData MoveSpeed => new SpeedData();
        protected override float Gravity => _definition.Gravity;
        protected override bool CanTurn => CANT_TURN;

        public DamageState(KlonoaBehaviour behaviour) : base(behaviour) { }

        public void SetStates(KlonoaState nextState)
        {
            _nextState = nextState;
        }

        public void SetDamageHit(RaycastHit hit)
        {
            _hit = hit;
        }

        public override void Enter()
        {
            _timer = 0;
            _behaviour.StartInvincibility(_definition.StunnedTime);
            DamageImpulse();
        }

        public void DamageImpulse()
        {
            Vector3 directionToHit = _hit.point - _behaviour.transform.position;
            float direction = Vector3.Dot(_behaviour.transform.forward, directionToHit);
            Vector3 knockbackDirection = _definition.KnockbackDirection;
            knockbackDirection = new Vector3(
                knockbackDirection.x,
                 knockbackDirection.y * 1.5f,
                  knockbackDirection.z * Mathf.Sign(direction));
            _mover.Velocity = knockbackDirection * _definition.KnockbackForce;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _timer += deltaTime;
            if (_timer >= _definition.StunnedTime)
            {
                ChangeState(_nextState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            _mover.Velocity = Vector3.zero;
        }

        public override void AttackAction() { }
        public override void JumpAction() { }
        public override void JumpKeepAction() { }
        public override void JumpReleaseAction() { }
    }
}