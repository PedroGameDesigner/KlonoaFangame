using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class GizmoExtensions
    {
        public static void DrawAsGizmo(this Collider collider)
        {
            switch (collider)
            {
                case CapsuleCollider capsule:
                    DrawCapsule(capsule);
                    break;
            }
        }

        private static void DrawCapsule(CapsuleCollider capsule)
        {
            Vector3 center = capsule.transform.position + capsule.center;
            float capCenterDistance = capsule.height / 2 - capsule.radius;
            Gizmos.DrawWireSphere(center + Vector3.up * capCenterDistance, capsule.radius);
            Gizmos.DrawWireSphere(center + Vector3.down * capCenterDistance, capsule.radius);

            Gizmos.DrawLine(Vector3.left * capsule.radius + center + Vector3.up * capCenterDistance,
                            Vector3.left * capsule.radius + center + Vector3.down * capCenterDistance);
            Gizmos.DrawLine(Vector3.right * capsule.radius + center + Vector3.up * capCenterDistance,
                            Vector3.right * capsule.radius + center + Vector3.down * capCenterDistance);
            Gizmos.DrawLine(Vector3.forward * capsule.radius + center + Vector3.up * capCenterDistance,
                            Vector3.forward * capsule.radius + center + Vector3.down * capCenterDistance);
            Gizmos.DrawLine(Vector3.back * capsule.radius + center + Vector3.up * capCenterDistance,
                            Vector3.back * capsule.radius + center + Vector3.down * capCenterDistance);
        }
    }
}
