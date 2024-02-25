using Cameras;
using Cinemachine;
using Gameplay.Klonoa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneState : MonoBehaviour
{
    private const float SCREEN_CENTER = 0.5f;

    [SerializeField] private float extraDeathZoneHeight = 0.05f;

    private CinemachineFramingTransposer transposer;
    private CameraTarget target;

    private float screenY;
    private float deathZoneSize;
    private bool isGrounded;
    private StateType currentStateType = StateType.Normal;

    private void Awake()
    {
        var vcam = GetComponent<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        target = vcam.Follow.GetComponent<CameraTarget>();

        screenY = transposer.m_ScreenY;
        deathZoneSize = Mathf.Abs(SCREEN_CENTER - screenY) + extraDeathZoneHeight;
    }

    private void LateUpdate()
    {
        if (currentStateType != StateType.Normal || target.Klonoa == null)
            return;

        if (!isGrounded && target.Klonoa.IsGrounded) //Landing
            DisableDeathZone();
        else if (isGrounded && !target.Klonoa.IsGrounded) //leaving ground
            EnableDeathZone();

        isGrounded = target.Klonoa.IsGrounded;
    }

    private void OnEnable()
    {
        if (target.Klonoa == null)
        {
            Debug.Log("NO KLONOA");
            return;
        }

        target.Klonoa.BeginHangingEvent += OnBeginHanging;
        target.Klonoa.EndHangingEvent += OnEndHanging;
    }

    private void OnDisable()
    {
        if (target.Klonoa == null) return;

        target.Klonoa.BeginHangingEvent -= OnBeginHanging;
        target.Klonoa.EndHangingEvent -= OnEndHanging;
    }

    //private void OnJump() => BeginAlwaysActive(jumpActiveTime);
    private void OnBeginHanging() => BeginAlwaysInactive();
    private void OnEndHanging() => SetNormalState();


    private void BeginAlwaysInactive()
    {
        currentStateType = StateType.AlwaysInactive;
        DisableDeathZone();
        isGrounded = true;
    }

    private void SetNormalState()
    {
        currentStateType = StateType.Normal;
    }

    private void DisableDeathZone()
    {
        transposer.m_ScreenY = screenY;
        transposer.m_DeadZoneHeight = 0;
    }

    private void EnableDeathZone()
    {
        transposer.m_ScreenY = SCREEN_CENTER;
        transposer.m_DeadZoneHeight = deathZoneSize;
    }

    private enum StateType { Normal, AlwaysActive, AlwaysInactive}
}
