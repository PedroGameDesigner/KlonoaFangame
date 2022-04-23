using System.Collections;
using System.Collections.Generic;
using Gameplay.Klonoa;
using UnityEngine;

namespace Gameplay
{
    public class RotateWithFacing : MonoBehaviour
    {
        [SerializeField] private KlonoaBehaviour _behaviour;
        private KlonoaBehaviour.FaceDirection _facingProxy;
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

        private void UpdatePosition(KlonoaBehaviour.FaceDirection facing)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.x = _originPosition.x * FacingDirection(facing).z;
            newPosition.z = _originPosition.z * FacingDirection(facing).z;
            transform.localPosition = newPosition;
        }

        private Vector3 FacingDirection(KlonoaBehaviour.FaceDirection facing)
        {
            switch (facing)
            {
                case KlonoaBehaviour.FaceDirection.Right:
                    return Vector3.forward;
                case KlonoaBehaviour.FaceDirection.Left:
                    return Vector3.back;
                case KlonoaBehaviour.FaceDirection.Front:
                    return Vector3.right;
                case KlonoaBehaviour.FaceDirection.Back:
                    return Vector3.left;
                default:
                    return Vector3.zero;
            }
        }
    }
}
