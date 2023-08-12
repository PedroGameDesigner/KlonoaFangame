using CylinderCharacterController;
using UnityEngine;

namespace Gameplay
{
    public class ScaleWithCollider : MonoBehaviour
    {
        [SerializeField] private CylinderCollider _collider = null;

        private SpriteRenderer _renderer;
        private float _baseHeight;
        private float _baseWide;
        private float _proxyHeight;

        // Start is called before the first frame update
        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _baseHeight = _collider.Height;
            _baseWide = _collider.Radius * 2f;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_collider.enabled)
            {
                if (_collider.Height != _proxyHeight)
                {
                    transform.localPosition = Vector3.up * _collider.Height * 0.5f;
                    float deform = (_baseHeight - _collider.Height / _baseHeight) * 0.5f;
                    float wide = (_collider.Radius * 2) / _baseWide + deform;
                    transform.localScale = new Vector3(wide, _collider.Height / _baseHeight, wide);
                }
                _proxyHeight = _collider.Height;
            }
            _renderer.enabled =_collider.enabled;
        }
    }
}