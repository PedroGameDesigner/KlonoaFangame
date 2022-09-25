using Gameplay.Enemies.Ball;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public interface ICapturable
    {
        Transform transform { get; }
        bool CanBeCaptured { get; }

        void Capture();
    }
}
