using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies.Ball
{
    public class EnemyBallParticles : MonoBehaviour
    {
        //Constants
        private const string REFERENCES_GROUP = "References";
        private const string PARTICLES_GROUP = "Particles Clips";

        //Attributes
        [FoldoutGroup(REFERENCES_GROUP), SerializeField] private EnemyBall _behaviour = null;

        [FoldoutGroup(PARTICLES_GROUP), SerializeField] private ParticleSystem _destroyParticles;
        [FoldoutGroup(PARTICLES_GROUP), SerializeField] private ParticleSystem _captureParticles;

        private void Awake()
        {
            _behaviour.DestroyEvent += OnDestroyEvent;
            _behaviour.TransitionFinishEvent += OnTransitionFinish;
            _behaviour.ThrownEvent += OnThrowEvent;
        }

        private void OnDestroyEvent()
        {
            _destroyParticles.Play(true);
            _captureParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        private void OnTransitionFinish()
        {
            _captureParticles.Play(true);
        }

        private void OnThrowEvent(Vector3 direction)
        {
            _captureParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}