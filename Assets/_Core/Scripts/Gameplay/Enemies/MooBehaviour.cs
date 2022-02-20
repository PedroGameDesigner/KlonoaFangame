using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class MooBehaviour : EnemyBehaviour
    {
        private const bool IS_CAPTURABLE = true;
        public override bool IsCapturable => IS_CAPTURABLE;
    }
}
