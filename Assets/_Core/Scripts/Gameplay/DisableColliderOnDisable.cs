using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class DisableColliderOnDisable : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _referenceBehaviour;
        [SerializeField] private Collider _colliderToDisable;

        private bool _stateProxy;

        private void Start()
        {
            _stateProxy = _referenceBehaviour.enabled;
        }

        private void Update()
        {
            if (_stateProxy != _referenceBehaviour.enabled)
            {
                _colliderToDisable.enabled = _referenceBehaviour.enabled;
                _stateProxy = _referenceBehaviour.enabled;
            }
        }
    }
}