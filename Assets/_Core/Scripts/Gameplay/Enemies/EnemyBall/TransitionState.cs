using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies.Ball
{
    public class TransitionState : State<EnemyBall>
    {
        protected State<EnemyBall> _nextState;

        public TransitionState(EnemyBall behaviour) : base(behaviour) { }
        public override void Enter()
        {
            _behaviour.TransitionFinishEvent += OnTransitionFinished;
            _behaviour.IsSolid = false;
            _behaviour.CollideWithGround = false;
            _behaviour.CollideWithEnemy = false;
            _behaviour.FollowPath = false;
            _behaviour.ClimbSlope = false;
        }

        public void SetNextState(State<EnemyBall> nextState)
        {
            _nextState = nextState;
        }

        private void OnTransitionFinished()
        {
            ChangeState(_nextState);
        }


        public override void Update(float deltaTime) { }

        public override void FixedUpdate(float deltaTime) { }

        public override void LateUpdate(float deltaTime) { }

        public override void Exit()
        {
            _behaviour.TransitionFinishEvent -= OnTransitionFinished;
        }

        public override void DrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_behaviour.Position, 0.5f);
        }
    }
}