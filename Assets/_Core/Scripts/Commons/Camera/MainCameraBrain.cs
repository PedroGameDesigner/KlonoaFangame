using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cameras
{
    public class MainCameraBrain : MonoBehaviour
    {
        private const int HIGH_PRIORITY = 10;
        private const int LOW_PRIORITY = 0;

        [SerializeField] private CameraChangeDollyWithPath _pathCamera;
        [SerializeField] private CameraStatic[] _staticCameras;
        [Space]
        [SerializeField] private CinemachineVirtualCamera _firstCamera;

        private CameraType _currentCameraType = CameraType.Dolly;
        private CinemachineVirtualCamera _currentCamera;
        private CinemachineVirtualCamera _currentDollyCamera;
        private CameraStatic _currentStaticCamera;

        private void Awake()
        {
            _pathCamera.CameraChangeEvent += OnPathCameraChange;
            foreach(CameraStatic camera in _staticCameras)
            {
                camera.EnterAreaEvent += OnEnterStaticCameraArea;
            }

            _currentDollyCamera = _firstCamera;
            _currentDollyCamera.Priority = HIGH_PRIORITY;
        }

        private void OnEnterStaticCameraArea(CameraStatic newCamera)
        {
            if (_currentStaticCamera != null)
            {
                _currentStaticCamera.ExitAreaEvent -= OnExitStaticCameraArea;
            }

            _currentStaticCamera = newCamera;
            _currentStaticCamera.ExitAreaEvent += OnExitStaticCameraArea;
            _currentCameraType = CameraType.Static;
            UpdateActiveCamera();
        }

        private void OnExitStaticCameraArea(CameraStatic newCamera)
        {
            _currentCameraType = CameraType.Dolly;
            UpdateActiveCamera();
        }

        private void OnPathCameraChange(CinemachineVirtualCamera newCamera)
        {
           if (newCamera != _currentDollyCamera)
            {
                if (_currentDollyCamera != null)
                    _currentDollyCamera.Priority = LOW_PRIORITY;
                _currentDollyCamera = newCamera;
                UpdateActiveCamera();
            }
        }

        private void UpdateActiveCamera()
        {
            if (_currentCamera != null)
                _currentCamera.Priority = LOW_PRIORITY;

            switch (_currentCameraType)
            {
                case CameraType.Dolly:
                    _currentCamera = _currentDollyCamera;
                    break;
                case CameraType.Static:
                    _currentCamera = _currentStaticCamera.Camera;
                    break;
            }

            _currentCamera.Priority = HIGH_PRIORITY;
        }

        private enum CameraType
        {
            Dolly,
            Static
        }
    }
}
