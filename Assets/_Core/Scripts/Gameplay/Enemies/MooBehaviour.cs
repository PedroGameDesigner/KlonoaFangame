using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class MooBehaviour : EnemyBehaviour
    {
        public override bool CanBeCaptured => true;
    }
}
