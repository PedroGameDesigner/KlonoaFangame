using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ScaleWithCollider : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider = null;

        private Vector3 _baseSize;
        private Vector3 _proxySize;

        // Start is called before the first frame update
        private void Awake()
        {
            _baseSize = _collider.size;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_collider.bounds.size != _proxySize)
            {                
                transform.localPosition = _collider.center;
                float deform = (1 -  _collider.size.y / _baseSize.y) * 0.5f;
                transform.localScale = new Vector3(
                    _collider.size.x / _baseSize.x + deform,
                    _collider.size.y / _baseSize.y,
                    _collider.size.z / _baseSize.z + deform);
            }
            _proxySize = _collider.bounds.size;
            gameObject.SetActive(_collider.enabled);
        }
    }
}