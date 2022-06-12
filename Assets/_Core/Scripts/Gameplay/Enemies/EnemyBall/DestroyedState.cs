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
            Debug.Log("EnemyBall Destroy Self");
            _behaviour.SetVelocity(0);
            _behaviour.transform.parent = null;
            _behaviour.Collider.enabled = false;
            _behaviour.CollisionEnabled = false;
            GameObject.Destroy(_behaviour.gameObject, _behaviour.DestroyDelay);
        }

        public override void FixedUpdate(float deltaTime) { }
        public override void Update(float deltaTime) { }
        public override void LateUpdate(float deltaTime) { }
        public override void Exit() { }
        public override void DrawGizmos() { }
    }
}