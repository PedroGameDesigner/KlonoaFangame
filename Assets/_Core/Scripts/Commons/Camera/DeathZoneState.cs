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
    private KlonoaBehaviour klonoa;

    private float screenY;
    private float deathZoneSize;
    private bool isGrounded;
    private StateType currentStateType = StateType.Normal;

    private void Awake()
    {
        var vcam = GetComponent<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();

        var target = vcam.Follow.GetComponent<CameraTarget>();
        klonoa = target.Klonoa;

        screenY = transposer.m_ScreenY;
        deathZoneSize = Mathf.Abs(SCREEN_CENTER - screenY) + extraDeathZoneHeight;
    }

    private void LateUpdate()
    {
        if (currentStateType != StateType.Normal)
            return;

        if (!isGrounded && klonoa.IsGrounded) //Landing
            DisableDeathZone();
        else if (isGrounded && !klonoa.IsGrounded) //leaving ground
            EnableDeathZone();

        isGrounded = klonoa.IsGrounded;
    }

    private void OnEnable()
    {
        klonoa.StateChangeEvent += OnKlonoaStateChange;        
    }

    private void OnDisable()
    {
        klonoa.StateChangeEvent -= OnKlonoaStateChange;
    }

    private void OnKlonoaStateChange()
    {

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
