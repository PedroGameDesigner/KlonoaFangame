using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class SpikerBehaviour : EnemyBehaviour
    {
        public override bool CanBeCaptured => false;

        public override void DoDamage()
        {
            // Method intentionally left empty.
        }
    }
}