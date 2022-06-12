using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyParticles : MonoBehaviour
    {
        [SerializeField] private EnemyBehaviour _behaviour;
        [Space]
        [SerializeField] private ParticleSystem _deathParticles;

        private void Awake()
        {
            _behaviour.DeathEvent += OnDeath;
        }

        private void OnDeath()
        {
            _deathParticles.Play(true);
        }
    }
}
