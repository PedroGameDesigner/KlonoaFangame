using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.Enemies.Ball
{
    public class PathFlyState : State<EnemyBall>
    {
        private readonly float _travelTime;

        private float _traveledTime = 0;
        State<EnemyBall> _nextState;

        public PathFlyState(EnemyBall behaviour, State<EnemyBall> nextState, float travelTime) : base(behaviour)
        {
            _nextState = nextState;
            _travelTime = travelTime;
        }

        public override void Enter()
        {
            _traveledTime = 0;
            _behaviour.FollowPath = true;
            _behaviour.ClimbSlope = true;
            _behaviour.SelectedCollisionType = EnemyBall.CollisionType.All;
            _behaviour.CollisionEnabled = true;
            _behaviour.SetVelocity(_behaviour.FlySpeed);
        }

        public override void FixedUpdate(float deltaTime)
        {
            _traveledTime += deltaTime;

            if (_traveledTime >= _travelTime)
            {
                ChangeState(_nextState);
            }
        }

        public override void LateUpdate(float deltaTime) { }

        public override void Update(float deltaTime) { }

        public override void Exit() { }

        public override void DrawGizmos()
        {
        }
    }
}
