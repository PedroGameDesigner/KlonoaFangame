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
        [SerializeField] private float _transitionTime;

        [Header("Vertial Deathzone Settings")]
        [SerializeField] private bool _followOnAir = true;
        [SerializeField] private bool _followOnGround = true;
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
            _target.FollowOnAir = _followOnAir;
            _target.FollowOnGround = _followOnGround;
            _target.UpdateRelativePosition(DistanceToTarget, _transitionTime);
        }
    }
}