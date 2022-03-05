using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace Gameplay.Enemies.Ball
{
    public class FreeFlyState : State<EnemyBall>
    {
        private float _traveledTime = 0;

        public FreeFlyState(EnemyBall behaviour) : base(behaviour) { }

        public override void Enter()
        {
            _traveledTime = 0;
            _behaviour.FollowPath = false;
            _behaviour.Speed(_behaviour.FlySpeed);
        }

        public override void FixedUpdate(float deltaTime) 
        {
            _traveledTime += _behaviour.FlySpeed * deltaTime;

            if (_traveledTime >= _behaviour.FreeFlyTime)
            {
                _behaviour.DestroySelf();
            }
        }

        public override void LateUpdate(float deltaTime) { }
        public override void Update(float deltaTime) { }
        public override void Exit() { }

        public override void DrawGizmos() { }
    }
}