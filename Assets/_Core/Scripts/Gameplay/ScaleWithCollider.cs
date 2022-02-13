using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ScaleWithCollider : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;

        private Vector3 _baseSize;
        private Vector3 _proxySize;

        // Start is called before the first frame update
        private void Awake()
        {
            _baseSize = _collider.bounds.size;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_collider.bounds.size != _proxySize)
            {
                transform.localPosition = _collider.center;
                transform.localScale = new Vector3(
                    _collider.bounds.size.x / _baseSize.x,
                    _collider.bounds.size.y / _baseSize.y,
                    _collider.bounds.size.z / _baseSize.z);
            }
            _proxySize = _collider.bounds.size;
        }
    }
}