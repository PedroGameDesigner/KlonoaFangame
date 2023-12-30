using Cinemachine;
using UnityEngine;

namespace Cameras
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraReference : MonoBehaviour
    {
        private const int SELECTED_PRIORITY = 100;
        private const int UNSELECTED_PRIORITY = 0;
        private const int DEFAULT_PRIORITY = 10;

        private static CameraReference CurrentCamera = null;
        private static CameraReference PreviousCamera = null;

        [SerializeField] private bool _defaultCamera;

        private CinemachineVirtualCamera _camera;

        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
        }

        private void OnValidate()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
        }

        public void ToggleCamera()
        {
            if (CurrentCamera != this)
            {
                PreviousCamera = CurrentCamera;
                CurrentCamera = this;
            }
            else
            {
                CurrentCamera = PreviousCamera;
                PreviousCamera = this;
            }

            if (CurrentCamera != null) CurrentCamera.SetSelected();
            if (PreviousCamera != null) PreviousCamera.SetUnselected();
        }

        public void SetSelected()
        {
            _camera.Priority = SELECTED_PRIORITY;
        }

        public void SetUnselected()
        {
            if (_defaultCamera)
                _camera.Priority = DEFAULT_PRIORITY;
            else
                _camera.Priority = UNSELECTED_PRIORITY;
        }
    }
}