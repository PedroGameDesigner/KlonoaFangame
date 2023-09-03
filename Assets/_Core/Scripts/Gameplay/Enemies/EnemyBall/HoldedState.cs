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
            _behaviour.IsSolid = true;
            _behaviour.CollideWithEnemy = true;
            _behaviour.CollideWithGround = false;
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
            float height = _behaviour.ColliderHeight;

            if (newHeight < height)
                ReduceColliderSize(newHeight);
            else
                RegrowColliderSize(newHeight);
        }

        private void ReduceColliderSize(float newHeight)
        {
            _behaviour.ChangeColliderHeight(newHeight);
        }

        private void RegrowColliderSize(float newHeight)
        {
            float height = _behaviour.ColliderHeight;
            float HeightDiference = Mathf.Min(newHeight, _behaviour.BaseHeight) - height;
            float regrowAmount = Mathf.Min(HeightDiference, _behaviour.RegrowSpeed * Time.deltaTime);
            float actualHeight = height + regrowAmount;
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
            _behaviour.ChangeColliderHeight(_behaviour.BaseHeight);
            _behaviour.ThrownEvent -= OnThrown;
        }

        public override void DrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_behaviour.Position, 0.5f);
        }
    }
}
