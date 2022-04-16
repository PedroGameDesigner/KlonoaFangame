using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Extensions
{
    public static class CapsuleColliderExtension
    {
        //CapsuleCollider
        public static int Cast(this CapsuleCollider collider, Vector3 direction, out RaycastHit[] results, float distance = Mathf.Infinity)
        {
            Vector3[] points = collider.Points();
            results = Physics.CapsuleCastAll(points[0], points[1], collider.radius, direction, distance);
            return results.Length;
        }

        public static int Cast(this CapsuleCollider collider, Vector3 direction, LayerMask layerMask, out RaycastHit[] results, float distance = Mathf.Infinity)
        {
            Vector3[] points = collider.Points();
            results = Physics.CapsuleCastAll(points[0], points[1], collider.radius, direction, distance, layerMask);
            return results.Length;
        }

        public static Vector3[] Points(this CapsuleCollider collider)
        {
            Vector3[] points = new Vector3[2];
            float pointDistance = Mathf.Max(0, collider.height * 0.5f - collider.radius);
            Vector3 direction = Vector3.zero;
            switch (collider.direction)
            {
                case 0: //X Axis
                    direction = Vector3.right;
                    break;
                case 1: //Y Axis
                    direction = Vector3.up;
                    break;
                case 2: //Z Axis
                    direction = Vector3.forward;
                    break;
            }

            points[0] = collider.transform.position + collider.center + (direction * pointDistance);
            points[1] = collider.transform.position + collider.center - (direction * pointDistance);

            return points;
        }
    }
}