using Gameplay.Klonoa;
using PlatformerRails;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cameras
{
    [ExecuteAlways]
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private KlonoaBehaviour _target;
        [SerializeField] private Vector3 _displacement;
        [SerializeField] private Quaternion _rotation;

        private List<DisplacementEntry> displacements = new List<DisplacementEntry>();
        private Vector3 _extraDisplacement = Vector3.zero;
        private Quaternion _extraRotation = Quaternion.identity;

        public KlonoaBehaviour Klonoa => _target;

        private void Awake()
        {
            if (_target == null)
                _target = FindObjectOfType<KlonoaBehaviour>();
        }

        private void LateUpdate()
        {
            transform.rotation = _extraRotation * _rotation * _target.transform.rotation;
            transform.position = _target.transform.position + transform.rotation * (_displacement + _extraDisplacement);
        }

        public void AddDisplacement(Transform caller, Vector3 position, Quaternion rotation)
        {
            for (int i = 0; i < displacements.Count; i++)
                if (displacements[i].caller == caller) return;
            
            var displacement = new DisplacementEntry();
            displacement.caller = caller;
            displacement.position = position;
            displacement.rotation = rotation;
            displacements.Add(displacement);

            _extraDisplacement += position;
            _extraRotation = rotation * _extraRotation;
        }

        public void RemoveDisplacement(Transform caller)
        {
            for (int i = 0; i < displacements.Count; i++)
            {
                if (displacements[i].caller == caller)
                {
                    _extraDisplacement -= displacements[i].position;
                    _extraRotation = Quaternion.Inverse(displacements[i].rotation) * _extraRotation;
                    displacements.Remove(displacements[i]);
                    break;
                }
            }
        }

        private struct DisplacementEntry
        {
            public Transform caller;
            public Vector3 position;
            public Quaternion rotation;
        }
    }
}