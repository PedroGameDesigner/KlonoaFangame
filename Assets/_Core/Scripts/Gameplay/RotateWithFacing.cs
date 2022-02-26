using System.Collections;
using System.Collections.Generic;
using Gameplay.Klonoa;
using UnityEngine;

namespace Gameplay
{
    public class RotateWithFacing : MonoBehaviour
    {
        [SerializeField] private KlonoaBehaviour _behaviour;
        private float _facingProxy;
        private Vector3 _originPosition;

        private void Awake()
        {
            _originPosition = transform.localPosition;
        }

        private void FixedUpdate()
        {
            if (_facingProxy != _behaviour.Facing)
            {
                UpdatePosition(_behaviour.Facing);
                _facingProxy = _behaviour.Facing;
            }
        }

        private void UpdatePosition(float facing)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.x = _originPosition.x * facing;
            newPosition.z = _originPosition.z * facing;
            transform.localPosition = newPosition;
        }
    }
}