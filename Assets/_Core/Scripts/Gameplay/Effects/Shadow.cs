using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Effects
{
    [ExecuteAlways]
    public class Shadow : MonoBehaviour
    {
        [SerializeField] private Projector _projector;
        [SerializeField] private float _baseSize = 1;
        [SerializeField] private float _maxDistance = 1;

        private RaycastHit _raycastHit = new RaycastHit();
        private float _distance;

        private Vector3 ProjectorDirection => transform.rotation * Vector3.forward;
        private LayerMask EffectLayer => -_projector.ignoreLayers;
        private Ray Ray => new Ray(transform.position, ProjectorDirection);


        private bool Initialized => _projector != null;


        private void Update()
        {
            if (!Initialized) return;
            bool hasHit = Physics.Raycast(Ray, out _raycastHit, _maxDistance, EffectLayer);
            if (hasHit)
            {
                CalculateShadowSize(_raycastHit.distance);
            }
            else
            {
                CalculateShadowSize(float.PositiveInfinity);
            }
        }

        private void CalculateShadowSize(float distance)
        {
            _distance = distance;
            _projector.orthographicSize = _baseSize * Mathf.Clamp01( 1 -(distance / _maxDistance));
        }

    }
}
