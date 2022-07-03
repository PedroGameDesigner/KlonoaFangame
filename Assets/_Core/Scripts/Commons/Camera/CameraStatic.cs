using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

namespace Cameras
{
    public class CameraStatic : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        private bool _targetInside;

        public CinemachineVirtualCamera Camera => _camera;


        public event Action<CameraStatic> EnterAreaEvent;
        public event Action<CameraStatic> ExitAreaEvent;


        //Asigned on Editor
        public void OnEntranceCrossed(EntranceTrigger entrance)
        {
            if (!_targetInside) OnTargetEnter();
            else OnTargetExit();
        }

        private void OnTargetEnter()
        {
            _targetInside = true;
            EnterAreaEvent?.Invoke(this);
        }

        private void OnTargetExit()
        {
            _targetInside = false;
            ExitAreaEvent?.Invoke(this);
        }
    }
}