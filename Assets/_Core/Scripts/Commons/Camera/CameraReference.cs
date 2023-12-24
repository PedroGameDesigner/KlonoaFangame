using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private float _transitionTime;

        [Header("Horizontal Settings")]
        [SerializeField] private float _horizontalDistanceProportion;

        [Header("Vertial Settings")]
        [SerializeField] private bool _followOnAir = true;
        [SerializeField] private bool _followOnGround = true;
        [SerializeField] private bool _followOnEvent = true;
        [SerializeField] private Vector2 _deathZoneMargin;

        private CinemachineVirtualCamera _camera;
        private CameraTarget _target;
        private CinemachineFramingTransposer _cameraBody;

        private float DistanceToTarget => _cameraBody.m_CameraDistance;

        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            _cameraBody = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        private void OnValidate()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            _target = FindAnyObjectByType<CameraTarget>();
            _cameraBody = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
            _camera.m_Transitions.m_OnCameraLive.AddListener(OnCameraLive);
        }

        private void OnCameraLive(ICinemachineCamera cameraIn, ICinemachineCamera cameraOut)
        {
            /*
            _target.FollowOnAir = _followOnAir;
            _target.FollowOnGround = _followOnGround;
            _target.UpdateRelativePosition(DistanceToTarget, _transitionTime);*/
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