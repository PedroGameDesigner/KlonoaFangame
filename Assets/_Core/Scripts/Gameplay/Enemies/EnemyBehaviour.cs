using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Enemies.Ball;
using UnityEngine;

namespace Gameplay.Enemies
{
    public abstract class EnemyBehaviour : HoldableObject
    {
        protected const float RESPAWN_TIME = 0.5f;

        [SerializeField] protected EnemyBall _ballPrefab = null;

        protected EnemyBall SpawnedBall { get; set; }

        public event Action DeathEvent;

        public override EnemyBall GetHoldedVersion(Transform holderTransform)
        {
            SpawnedBall = Instantiate(_ballPrefab, holderTransform.position, holderTransform.rotation);
            SpawnedBall.AssignHolder(holderTransform);
            SpawnedBall.DestroyEvent += OnBallDestroyed;
            return SpawnedBall;
        }

        public virtual void Kill()
        {
            Enable(false);
            DeathEvent?.Invoke();
        }

        protected void OnBallDestroyed()
        {
            SpawnedBall = null;
            Enable(true); 
        }
    }
}
