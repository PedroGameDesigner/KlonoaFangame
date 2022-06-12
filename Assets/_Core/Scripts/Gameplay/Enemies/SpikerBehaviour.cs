using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class SpikerBehaviour : EnemyBehaviour
    {
        private const bool IS_CAPTURABLE = false;
        public override bool IsCapturable => IS_CAPTURABLE;

        public override void Kill()
        {
            // Method intentionally left empty.
        }

        public override void Capture()
        {
            // Method intentionally left empty.
        }
    }
}