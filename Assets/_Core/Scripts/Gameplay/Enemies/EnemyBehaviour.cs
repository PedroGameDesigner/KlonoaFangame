using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] protected EnemyBall _ballPrefab;
        public void Kill()
        {
            gameObject.SetActive(false);
        }

        internal EnemyBall InstantiateBall(Transform ballHolder)
        {
            return Instantiate(_ballPrefab, ballHolder);
        }
    }
}
