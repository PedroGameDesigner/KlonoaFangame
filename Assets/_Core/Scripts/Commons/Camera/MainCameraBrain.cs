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
        [SerializeField] private CinemachineVirtualCamera _firstCamera;

        private CameraType _currentCameraType = CameraType.Dolly;
        private CinemachineVirtualCamera _currentDollyCamera;

        private void Awake()
        {
            _pathCamera.CameraChangeEvent += OnPathCameraChange;

            _currentDollyCamera = _firstCamera;
            _currentDollyCamera.Priority = HIGH_PRIORITY;
        }

        private void UpdateActiveCamera()
        {
            switch (_currentCameraType)
            {
                case CameraType.Dolly:
                    _currentDollyCamera.Priority = HIGH_PRIORITY;
                    break;
            }
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

        private enum CameraType
        {
            Dolly,
            Static
        }
    }
}
