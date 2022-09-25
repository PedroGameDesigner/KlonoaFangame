using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class AnimationCurveExtension
    {
        public static float GetTime(this AnimationCurve curve)
        {
            return curve.keys[curve.length - 1].time;
        }
    }
}
