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
        private bool _destroyed = false;

        public FreeFlyState(EnemyBall behaviour, float travelTime) : base(behaviour) 
        {
            _travelTime = travelTime;
        }

        public override void Enter()
        {
            _traveledTime = 0;
            _behaviour.FollowPath = false;
            _behaviour.ClimbSlope = true;
            _behaviour.IsSolid = false;
            _behaviour.CollideWithEnemy = true;
            _behaviour.CollideWithGround = true;
            _behaviour.SetVelocity(_behaviour.FlySpeed);
        }

        public override void FixedUpdate(float deltaTime) 
        {
            _traveledTime += _behaviour.FlySpeed * deltaTime;

            if (_traveledTime >= _travelTime && !_destroyed)
            {
                _behaviour.DestroySelf();
                _destroyed = true;
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