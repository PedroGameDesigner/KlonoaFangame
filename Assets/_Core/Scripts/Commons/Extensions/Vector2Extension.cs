using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class Vector2Extension
    {
        public static float RandomValue(this Vector2 vector2)
        {
            return Random.Range(vector2[0], vector2[1]);
        }
    }
}
