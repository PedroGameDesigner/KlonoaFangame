using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] protected EnemyBall _ballPrefab = null;

        public abstract bool IsCapturable { get; }

        public virtual void Kill()
        {
            gameObject.SetActive(false);
        }

        internal EnemyBall InstantiateBall(Transform holderTransform, Rigidbody holderRigidbody)
        {
            EnemyBall ball = Instantiate(_ballPrefab, holderTransform.position, holderTransform.rotation);
            ball.AssignHolder(holderTransform);
            return ball;
        }
    }
}
