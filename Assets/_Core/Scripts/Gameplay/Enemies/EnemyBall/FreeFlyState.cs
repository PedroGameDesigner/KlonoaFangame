using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace Gameplay.Enemies.Ball
{
    public class FreeFlyState : State<EnemyBall>
    {
        private readonly float _travelTime;

        private float _traveledTime = 0;

        public FreeFlyState(EnemyBall behaviour, float travelTime) : base(behaviour) 
        {
            _travelTime = travelTime;
        }

        public override void Enter()
        {
            _traveledTime = 0;
            _behaviour.FollowPath = false;
            _behaviour.ClimbSlope = true;
            _behaviour.SelectedCollisionType = EnemyBall.CollisionType.All;
            _behaviour.CollisionEnabled = true;
            _behaviour.SetVelocity(_behaviour.FlySpeed);
        }

        public override void FixedUpdate(float deltaTime) 
        {
            _traveledTime += _behaviour.FlySpeed * deltaTime;

            if (_traveledTime >= _travelTime)
            {
                _behaviour.DestroySelf();
            }
        }

        public override void LateUpdate(float deltaTime) { }
        public override void Update(float deltaTime) { }
        public override void Exit() { }

        public override void DrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_behaviour.Position, 0.5f);
        }
    }
}