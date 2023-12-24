using Gameplay.Klonoa;
using PlatformerRails;
using System.Collections;
using UnityEngine;

namespace Cameras
{
    [ExecuteAlways]
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private KlonoaBehaviour _target;
        [SerializeField] private Vector3 _displacement;
        [SerializeField] private Quaternion _rotation;

        public KlonoaBehaviour Klonoa => _target;

        private void Awake()
        {
            if (_target == null)
                _target = FindObjectOfType<KlonoaBehaviour>();
        }

        private void LateUpdate()
        {
            transform.rotation = _rotation * _target.transform.rotation;
            transform.position = _target.transform.position + transform.rotation * _displacement;
        }
    }
}