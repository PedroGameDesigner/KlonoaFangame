using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Hitboxes
{
    public class HitData
    {
        public Vector3 Point { get; private set; }
        public Vector3 Normal { get; private set; }
        public Hitbox Hitbox { get; private set; }

        public HitData(Vector3 point, Vector3 normal, Hitbox hitbox)
        {
            Point = point;
            Normal = normal;
            Hitbox = hitbox;
        }
    }
}
