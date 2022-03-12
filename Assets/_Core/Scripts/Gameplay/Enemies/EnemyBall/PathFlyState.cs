using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.Enemies.Ball
{
    public class PathFlyState : State<EnemyBall>
    {
        private float _traveledTime = 0;
        State<EnemyBall> _nextState;

        public PathFlyState(EnemyBall behaviour, State<EnemyBall> nextState) : base(behaviour)
        {
            _nextState = nextState;
        }

        public override void Enter()
        {
            _traveledTime = 0;
            _behaviour.FollowPath = true;
            _behaviour.Velocity(_behaviour.FlySpeed);
        }

        public override void FixedUpdate(float deltaTime)
        {
            _traveledTime += deltaTime;

            if (_traveledTime >= _behaviour.FollowPathTime)
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
