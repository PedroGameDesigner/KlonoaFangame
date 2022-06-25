using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    [System.Serializable]
    public class CollisionData
    {
        private float _groundCheckLength;
        private float _ceilingCheckLength;
        private LayerMask _layer;

        private float? _groundDistance;
        private float? _ceilingDistance;

        public bool Grounded => _groundDistance != null;
        public bool TouchingCeiling => _ceilingDistance != null;

        public float MaxGroundDistance { get; private set; }
        public float MaxCeilingDistance { get; private set; }
        public float GroundDistance => _groundDistance.HasValue ? _groundDistance.Value : MaxGroundDistance;
        public float CeilingDistance => _ceilingDistance.HasValue ? _ceilingDistance.Value : MaxCeilingDistance;

        public CollisionData(float maxGroundDistance, float groundCheckLength, float maxCeilingDistance, float ceilingCheckLength, LayerMask layer)
        {
            MaxGroundDistance = maxGroundDistance;
            MaxCeilingDistance = maxCeilingDistance;
            _groundCheckLength = groundCheckLength;
            _ceilingCheckLength = ceilingCheckLength;
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

        public void CheckCeiling(Transform transform)
        {
            RaycastHit info;
            var hit = Physics.Raycast(transform.position, transform.up, out info, MaxCeilingDistance + _ceilingCheckLength, _layer);
            if (hit)
            {
                _ceilingDistance = info.distance;
            }
            else
            {
                _ceilingDistance = null;
            }
        }
    }
}