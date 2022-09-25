using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class SpikerBehaviour : EnemyBehaviour
    {
        public override bool CanBeCaptured => false;

        public override void Kill()
        {
            // Method intentionally left empty.
        }
    }
}