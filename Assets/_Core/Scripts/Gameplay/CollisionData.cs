using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    [System.Serializable]
    public class CollisionData
    {
        private float _groundCheckLength;
        private LayerMask _layer;

        private float? _groundDistance;
        public float MaxGroundDistance { get; private set; }
        public float GroundDistance => _groundDistance.HasValue ? _groundDistance.Value : MaxGroundDistance;

        public CollisionData(float maxGroundDistance, float groundCheckLength, LayerMask layer)
        {
            MaxGroundDistance = maxGroundDistance;
            _groundCheckLength = groundCheckLength;
            _layer = layer;
        }

        public void CheckGround(Transform transform)
        {
            RaycastHit info;
            var hit = Physics.Raycast(transform.position, -transform.up, out info, MaxGroundDistance + _groundCheckLength, _layer);
            if (hit)
            {
                _groundDistance = info.distance;
            }
            else
            {
                _groundDistance = null;
            }
        }
    }
}