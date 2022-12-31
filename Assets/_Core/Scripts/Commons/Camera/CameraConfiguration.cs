using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cameras
{
    [CreateAssetMenu(fileName ="New CameraConfig", menuName = "Camera Config")]
    public class CameraConfiguration : ScriptableObject
    {
        [SerializeField] private float _forwardDistance;
        [SerializeField] private float _verticalDisplacement;
        [SerializeField] private float _cameraDistance;


        [SerializeField] private AnimationCurve _horizontalAcceleration;
        [SerializeField] private AnimationCurve _verticalAcceleration;
        [SerializeField] private float _rotationSpeed;

        public Vector3 Displacement =>
            new Vector3(0, _verticalDisplacement, _forwardDistance);

        public float CameraDistance => _cameraDistance;

        public float RotationSpeed => _rotationSpeed;

        public float GetHorizontalSpeed(float distance)
        {
            return _horizontalAcceleration.Evaluate(distance);
        }

        public float GetVerticalSpeed(float distance)
        {
            return _verticalAcceleration.Evaluate(distance);
        }
    }
}
