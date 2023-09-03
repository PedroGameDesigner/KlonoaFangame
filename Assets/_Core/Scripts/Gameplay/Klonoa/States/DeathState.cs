using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class DeathState : KlonoaState
    {
        private const bool CANT_TURN = false;

        public override bool IsNormalState => true;

        protected override SpeedData MoveSpeed => _definition.NotMoveSpeed;
        protected override float Gravity => _definition.Gravity;
        protected override bool CanTurn => CANT_TURN;

        public DeathState(KlonoaBehaviour behaviour) : base(behaviour) { }


        public override void Enter()
        {
            base.Enter();
            _mover.Velocity = Vector3.zero;
            _behaviour.enabled = false;
        }
        public override void JumpAction() { }

        public override void JumpKeepAction() { }

        public override void JumpReleaseAction() { }

        public override void AttackAction() { }
    }
}