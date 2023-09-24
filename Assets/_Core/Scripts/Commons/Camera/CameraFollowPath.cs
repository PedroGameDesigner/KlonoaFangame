using Cinemachine;
using UnityEngine;
namespace Cameras
{
    [AddComponentMenu("")] // Hide in menu
#if UNITY_2018_3_OR_NEWER
    [ExecuteAlways]
#else
    [ExecuteInEditMode]
#endif
    [SaveDuringPlay]
    public class CameraFollowPath : CinemachineExtension
    {
        [SerializeField] private CameraConfiguration _config;
        [SerializeField] private CameraTarget _target;

        private float _translationPercent;
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;


        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (Application.isPlaying)
                UpdateCameraState(vcam, stage, ref state, deltaTime);
            else
                SetCameraState(vcam, stage, ref state);
        }

        private void UpdateCameraState(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            switch (stage)
            {
                case CinemachineCore.Stage.Body:
                    UpdateCameraBody(vcam, ref state, deltaTime);
                    break;
                case CinemachineCore.Stage.Aim:
                    UpdateCameraAim(vcam, ref state, deltaTime);
                    break;

            }
        }

        private void UpdateCameraBody(CinemachineVirtualCameraBase vcam, ref CameraState state, float deltaTime)
        {
            Vector3 pathNormal = _target.PathNormal;
            _targetPosition = _target.transform.position + pathNormal * _config.CameraDistance;

            Vector3 difference = _targetPosition - state.RawPosition;
            Vector3 translation = CalculateTranslationVelocity(difference) * deltaTime;
            state.RawPosition += translation;
            _translationPercent = translation.magnitude / difference.magnitude;
        }

        private Vector3 CalculateTranslationVelocity(Vector3 difference)
        {
            Vector3 planeDifference = Vector3.ProjectOnPlane(difference, Vector3.up);
            float yDifference = difference.y;

            return planeDifference.normalized * _config.GetHorizontalSpeed(planeDifference.magnitude)
                + Vector3.up * _config.GetVerticalSpeed(yDifference);
            
        }

        private void UpdateCameraAim(CinemachineVirtualCameraBase vcam, ref CameraState state, float deltaTime)
        {
            Vector3 VectorToTarget = _target.transform.position - state.FinalPosition;
            _targetRotation = Quaternion.LookRotation(VectorToTarget, Vector3.up);
            state.RawOrientation = Quaternion.Slerp(state.RawOrientation, _targetRotation, _translationPercent);
            /*
            state.RawOrientation = Quaternion.RotateTowards(state.RawOrientation, _targetRotation, 
                _config.RotationSpeed * deltaTime);
            */
        }

        private void SetCameraState(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state)
        {
            Vector3 pathNormal = _target.PathNormal;
            state.RawPosition = _target.transform.position + pathNormal * _config.CameraDistance;

            Vector3 VectorToTarget = _target.transform.position - state.FinalPosition;
            state.RawOrientation = Quaternion.LookRotation(VectorToTarget, Vector3.up);
        }

    }
}
