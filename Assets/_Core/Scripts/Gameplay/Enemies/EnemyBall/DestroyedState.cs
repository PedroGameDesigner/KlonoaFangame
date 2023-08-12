using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies.Ball
{
    public class DestroyedState : State<EnemyBall>
    {
        public DestroyedState(EnemyBall behaviour) : base(behaviour) {}

        public override void Enter()
        {
            _behaviour.SetVelocity(0);
            _behaviour.transform.parent = null;
            _behaviour.IsSolid = false;
            _behaviour.CollideWithGround = false;
            _behaviour.CollideWithEnemy = false;
            GameObject.Destroy(_behaviour.gameObject, _behaviour.DestroyDelay);
        }

        public override void FixedUpdate(float deltaTime) { }
        public override void Update(float deltaTime) { }
        public override void LateUpdate(float deltaTime) { }
        public override void Exit() { }
        public override void DrawGizmos() { }
    }
}