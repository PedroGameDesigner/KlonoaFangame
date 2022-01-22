using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    [System.Serializable]
    public class CollisionData
    {
        private float _maxGroundDistance;
        private float _groundCheckLength;

        private float? _groundDistance;

        public bool Grounded => _groundDistance != null;
        public float MaxGroundDistance => _maxGroundDistance;
        public float GroundDistance => _groundDistance.Value;

        public CollisionData(float maxGroundDistance, float groundCheckLength)
        {
            _maxGroundDistance = maxGroundDistance;
            _groundCheckLength = groundCheckLength;
        }

        public void CheckGround(Transform transform)
        {
            RaycastHit info;
            var hit = Physics.Raycast(transform.position, -transform.up, out info, _maxGroundDistance + _groundCheckLength);
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