using System.Collections;
using System.Collections.Generic;
using Gameplay.Klonoa;
using UnityEngine;

namespace Gameplay
{
    public class RotateWithFacing : MonoBehaviour
    {
        [SerializeField] private KlonoaBehaviour _behaviour;
        [SerializeField] private bool _rotatePosition = true;
        [SerializeField] private bool _rotateRotation = false;
        private FaceDirection _facingProxy;
        private Vector3 _originPosition;
        private Quaternion _originRotation;

        private void Awake()
        {
            _originPosition = transform.localPosition;
            _originRotation = transform.localRotation;
        }

        private void FixedUpdate()
        {
            if (_facingProxy != _behaviour.Facing)
            {
                UpdatePosition(_behaviour.Facing);
                UpdateRotation(_behaviour.Facing);
                _facingProxy = _behaviour.Facing;
            }
        }

        private void UpdatePosition(FaceDirection facing)
        {
            if (!_rotatePosition) return;
            transform.localPosition = facing.GetQuaternion() * _originPosition; 
        }

        private void UpdateRotation(FaceDirection facing)
        {
            if (!_rotateRotation) return;
            if ((int)facing % 2 == 0)
                transform.localRotation = _originRotation * Quaternion.AngleAxis(((int)facing) * 1.5f * 90, Vector3.up);
            else
                transform.localRotation = Quaternion.AngleAxis(90, Vector3.up);

        }
    }
}
