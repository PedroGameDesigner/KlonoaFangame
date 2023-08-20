using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class DisableProjectorOnDisable : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _referenceBehaviour;
        [SerializeField] private Projector _projectorToDisable;

        private bool _stateProxy;

        private void Start()
        {
            _stateProxy = _referenceBehaviour.enabled;
        }

        private void Update()
        {
            if (_stateProxy != _referenceBehaviour.enabled)
            {
                _projectorToDisable.enabled = _referenceBehaviour.enabled;
                _stateProxy = _referenceBehaviour.enabled;
            }
        }
    }
}
