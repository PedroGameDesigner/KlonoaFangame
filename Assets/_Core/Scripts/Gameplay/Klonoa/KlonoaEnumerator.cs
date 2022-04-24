using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public enum FaceDirection
    {
        Right = 0,
        Back = 1,
        Left = 2,
        Front = 3
    }

    public static class FaceDirectionExtensions
    {
        public static Vector3 GetVector(this FaceDirection face)
        {
            switch (face)
            {
                case FaceDirection.Right:
                    return Vector3.forward;
                case FaceDirection.Left:
                    return Vector3.back;
                case FaceDirection.Front:
                    return Vector3.left;
                case FaceDirection.Back:
                    return Vector3.right;
                default:
                    return Vector3.zero;
            }
        }
    }
}