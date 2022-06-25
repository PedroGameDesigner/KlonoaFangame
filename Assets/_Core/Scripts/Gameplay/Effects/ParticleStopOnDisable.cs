using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Effects
{
    public class ParticleStopOnDisable : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _behaviour;
        [SerializeField] private ParticleSystem _particles;

        private bool _stateProxy;

        private void Start()
        {
            _stateProxy = _behaviour.enabled;
        }

        private void Update()
        {
            if (_stateProxy && !_behaviour.enabled)
            {
                _particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                _stateProxy = _behaviour.enabled;
            }
        }
    }
}
