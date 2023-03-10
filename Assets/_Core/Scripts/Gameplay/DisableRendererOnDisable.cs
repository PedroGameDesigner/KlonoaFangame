using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class DisableRendererOnDisable : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _referenceBehaviour;
        [SerializeField] private Renderer _rendererToDisable;

        private bool _stateProxy;

        private void Start()
        {
            _stateProxy = _referenceBehaviour.enabled;
        }

        private void Update()
        {
            if (_stateProxy != _referenceBehaviour.enabled)
            {
                _rendererToDisable.enabled = _referenceBehaviour.enabled;
                _stateProxy = _referenceBehaviour.enabled;
            }
        }
    }
}
