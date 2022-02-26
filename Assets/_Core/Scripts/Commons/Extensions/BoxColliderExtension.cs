using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class BoxColliderExtension
    {
        public static int Cast(this BoxCollider collider, Vector3 direction, out RaycastHit[] results, float distance = Mathf.Infinity)
        {
            results = Physics.BoxCastAll(collider.transform.position + collider.center, collider.size * 0.5f, direction, collider.transform.rotation, distance);
            return results.Length;
        }

        public static int Cast(this BoxCollider collider, Vector3 direction, LayerMask layerMask, out RaycastHit[] results, float distance = Mathf.Infinity)
        {
            results = Physics.BoxCastAll(collider.transform.position + collider.center, collider.size * 0.5f, direction, collider.transform.rotation, distance, layerMask);
            return results.Length;
        }
    }
}
