using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class DisableRendererOnDisable : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _referenceBehaviour;
        [SerializeField] private Component[] _behavioursToDisable;

        private bool _stateProxy;

        private void Start()
        {
            _stateProxy = _referenceBehaviour.enabled;
        }

        private void Update()
        {
            if (_stateProxy != _referenceBehaviour.enabled)
            {
                foreach(Component component in _behavioursToDisable)
                    component.SendMessage("enabled", _referenceBehaviour.enabled, SendMessageOptions.DontRequireReceiver);
                _stateProxy = _referenceBehaviour.enabled;
            }
        }
    }
}
