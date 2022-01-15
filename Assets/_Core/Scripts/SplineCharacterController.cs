using Dreamteck;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineCharacterController : SplineTracer
{
    public enum Wrap { Default, Loop, PingPong }
    [HideInInspector]
    public Wrap wrapMode = Wrap.Default;
    
    private bool _autoStartPosition = true;
    private double lastClippedPercent;

    public Vector2 Speed { get; set; }

    protected override void Start()
    {
        base.Start();
        if (_autoStartPosition)
        {
            SetPercent(spline.Project(GetTransform().position).percent);
        }
    }

    protected override void LateRun()
    {
        base.LateRun();
#if UNITY_EDITOR
        if (!Application.isPlaying) return;
#endif
        Follow();
    }

    void Follow()
    {
        if (Speed.magnitude > 0)
            Debug.Log("Speed = " + Speed.ToString(), gameObject);
        float translation = Speed.x * Time.deltaTime;
        Move(translation);        
    }

    public void Move(double percent)
    {
        if (percent == 0.0) return;
        if (sampleCount <= 1)
        {
            if (sampleCount == 1)
            {
                _result.CopyFrom(GetSampleRaw(0));
                ApplyMotion();
            }
            return;
        }
        Evaluate(_result.percent, _result);
        double startPercent = _result.percent;
        if (wrapMode == Wrap.Default && lastClippedPercent >= 1.0 && startPercent == 0.0) 
            startPercent = 1.0;
        double percentaje = startPercent + (_direction == Spline.Direction.Forward ? percent : -percent);

        lastClippedPercent = percentaje;
        if (_direction == Spline.Direction.Forward && percentaje >= 1.0)
        {
            switch (wrapMode)
            {
                case Wrap.Default:
                    percentaje = 1.0;
                    break;
                case Wrap.Loop:
                    CheckTriggers(startPercent, 1.0);
                    CheckNodes(startPercent, 1.0);
                    while (percentaje > 1.0) percentaje -= 1.0;
                    startPercent = 0.0;
                    break;
                case Wrap.PingPong:
                    percentaje = DMath.Clamp01(1.0 - (percentaje - 1.0));
                    startPercent = 1.0;
                    _direction = Spline.Direction.Backward;
                    break;
            }
        }
        else if (_direction == Spline.Direction.Backward && percentaje <= 0.0)
        {
            switch (wrapMode)
            {
                case Wrap.Default:
                    percentaje = 0.0;
                    break;
                case Wrap.Loop:
                    CheckTriggers(startPercent, 0.0);
                    CheckNodes(startPercent, 0.0);
                    while (percentaje < 0.0) percentaje += 1.0;
                    startPercent = 1.0;
                    break;
                case Wrap.PingPong:
                    percentaje = DMath.Clamp01(-percentaje);
                    startPercent = 0.0;
                    _direction = Spline.Direction.Forward;
                    break;
            }
        }

        CheckTriggers(startPercent, percentaje);
        CheckNodes(startPercent, percentaje);
        Evaluate(percentaje, _result);
        ApplyMotion();
        
        InvokeTriggers();
        InvokeNodes();
    }
}
