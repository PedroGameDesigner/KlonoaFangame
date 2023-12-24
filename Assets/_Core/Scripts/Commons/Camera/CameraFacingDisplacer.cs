using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Gameplay.Klonoa;
using Cameras;

public class CameraFacingDisplacer : MonoBehaviour
{
    private const float SCREEN_CENTER = 0.5f;

    [SerializeField] private float transitionTime = 2f;

    private CinemachineFramingTransposer transposer;
    private KlonoaBehaviour klonoa;
    private Coroutine coroutine;
    private float absoluteDisplacement;
    private float lastFacing;
    private float facing;

    private void Awake()
    {
        var vcam = GetComponent<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();

        var target = vcam.Follow.GetComponent<CameraTarget>();
        klonoa = target.Klonoa;

        absoluteDisplacement = Mathf.Abs(SCREEN_CENTER - transposer.m_ScreenX);
        facing = klonoa.HorizontalFacing.GetVector().z;
        lastFacing = facing;
    }

    private void LateUpdate()
    {
        if (lastFacing != klonoa.HorizontalFacing.GetVector().z)
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(ChangeFacingCoroutine(facing, klonoa.HorizontalFacing.GetVector().z));
        }

        transposer.m_ScreenX = SCREEN_CENTER - facing * absoluteDisplacement;
    }

    private IEnumerator ChangeFacingCoroutine(float startFacing, float endFacing)
    {
        lastFacing = endFacing;
        facing = startFacing;
        float timer = 0;
        while (timer < transitionTime) 
        {
            timer += Time.deltaTime;
            facing = Mathf.Lerp(startFacing, endFacing, timer/ transitionTime);
            yield return null;
        }
        facing = endFacing;
    }
}
