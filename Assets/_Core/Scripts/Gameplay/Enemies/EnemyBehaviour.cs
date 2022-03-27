using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Enemies.Ball;
using UnityEngine;

namespace Gameplay.Enemies
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        protected const float RESPAWN_TIME = 0.5f;

        [SerializeField] protected EnemyBall _ballPrefab = null;

        public abstract bool IsCapturable { get; }
        protected EnemyBall SpawnedBall { get; set; }

        public virtual void Kill()
        {
            gameObject.SetActive(false);
        }

        internal EnemyBall InstantiateBall(Transform holderTransform, Rigidbody holderRigidbody)
        {
            SpawnedBall = Instantiate(_ballPrefab, holderTransform.position, holderTransform.rotation);
            SpawnedBall.AssignHolder(holderTransform);
            SpawnedBall.DestroyEvent += OnBallDestroyed;
            return SpawnedBall;
        }

        protected void OnBallDestroyed()
        {
            SpawnedBall = null;
            gameObject.SetActive(true); 
        }
    }
}
