using Cameras;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetReference : MonoBehaviour
{
    [SerializeField]
    private Vector2 _positionOfffset;
    [SerializeField]
    private Quaternion _rotationOfffset;

    private CameraTarget _target;
    private CinemachineVirtualCamera _camera;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _target = _camera.Follow.GetComponent<CameraTarget>();
    }

    private void OnEnable()
    {
        _target.AddDisplacement(transform, _positionOfffset, _rotationOfffset);
    }

    private void OnDisable()
    {
        _target.RemoveDisplacement(transform);
    }
}
