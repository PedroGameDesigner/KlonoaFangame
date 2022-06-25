using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace Gameplay.Enemies.Ball
{
    public class HoldedState : State<EnemyBall>
    {
        protected State<EnemyBall> _throwSideState;
        protected State<EnemyBall> _throwDownState;

        public HoldedState(EnemyBall behaviour, State<EnemyBall> throwSideState, State<EnemyBall> throwDownState) :base(behaviour)
        {
            _throwSideState = throwSideState;
            _throwDownState = throwDownState;
        }

        public override void Enter() 
        {
            _behaviour.ThrownEvent += OnThrown;
            _behaviour.SelectedCollisionType = EnemyBall.CollisionType.Enemies;
            _behaviour.CollisionEnabled = true;
            _behaviour.FollowPath = false;
            _behaviour.ClimbSlope = false;
        }

        public override void FixedUpdate(float deltaTime) { }
        public override void Update(float deltaTime) { }

        public override void LateUpdate(float deltaTime)
        {
            CalculateSize();
        }

        private void CalculateSize()
        {
            float newHeight = _behaviour.CheckCeilDistance();
            Vector3 size = _behaviour.ColliderSize;

            if (newHeight < size.y)
            {
                ReduceColliderSize(newHeight);
            }
            else
            {
                RegrowColliderSize(newHeight);
            }
        }

        private void ReduceColliderSize(float newHeight)
        {
            _behaviour.ChangeColliderHeight(newHeight);
        }

        private void RegrowColliderSize(float newHeight)
        {
            Vector3 size = _behaviour.ColliderSize;
            float sizeDiference = Mathf.Min(newHeight, _behaviour.BaseSize.y) - size.y;
            float regrowAmount = Mathf.Min(sizeDiference, _behaviour.RegrowSpeed * Time.deltaTime);
            float actualHeight = size.y + regrowAmount;
            _behaviour.ChangeColliderHeight(actualHeight);
        }

        private void OnThrown(Vector3 direction)
        {
            if (direction.y != 0f) 
            {
                ChangeState(_throwDownState);
            }
            else
            {
                ChangeState(_throwSideState);
            }
        }

        public override void Exit()
        {
            _behaviour.ChangeColliderHeight(_behaviour.BaseSize.y);
            _behaviour.ThrownEvent -= OnThrown;
        }

        public override void DrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_behaviour.Position, 0.5f);
        }
    }
}
