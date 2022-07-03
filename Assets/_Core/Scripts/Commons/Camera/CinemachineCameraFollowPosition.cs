using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;
using Cinemachine;

namespace Cameras
{
    [AddComponentMenu("")] // Hide in menu
    #if UNITY_2018_3_OR_NEWER
    [ExecuteAlways]
    #else
    [ExecuteInEditMode]
    #endif
    [SaveDuringPlay]
    public class CinemachineCameraFollowPosition : CinemachineExtension
    {
        [SerializeField] private CinemachineCore.Stage _applyAfter = CinemachineCore.Stage.Aim;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == _applyAfter)
            {
                Vector3 screenOffset = Vector3.zero;

                Vector3 followedPosition = vcam.Follow.position;
                float extraHeight = followedPosition.y - state.RawPosition.y;
                state.PositionCorrection += Vector3.up * extraHeight;

                var q = Quaternion.LookRotation(
                    state.ReferenceLookAt - state.CorrectedPosition, state.ReferenceUp);
                q = q.ApplyCameraRotation(-screenOffset, state.ReferenceUp);
                state.RawOrientation = q;
            }
        }
    }
}