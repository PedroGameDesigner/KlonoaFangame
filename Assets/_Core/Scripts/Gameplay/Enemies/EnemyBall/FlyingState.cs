using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace Gameplay.Enemies.Ball
{
    public class FlyingState : State<EnemyBall>
    {
        private float _traveledDistance = 0;

        public FlyingState(EnemyBall behaviour) : base(behaviour) { }

        public override void Enter()
        {
            _traveledDistance = 0;
        }

        public override void Exit() { }

        public override void FixedUpdate(float deltaTime) 
        {
            Vector3 translation = _behaviour.FlyVelocity * deltaTime;
            _traveledDistance += translation.magnitude;
            _behaviour.transform.Translate(translation, Space.World);

            if (_traveledDistance >= _behaviour.MaxFlyDistance)
            {
                _behaviour.DestroySelf();
            }
        }

        public override void LateUpdate(float deltaTime) { }
        public override void Update(float deltaTime) { }

        public override void DrawGizmos() { }
    }
}