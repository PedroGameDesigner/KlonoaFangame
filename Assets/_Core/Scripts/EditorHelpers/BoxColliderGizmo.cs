using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorHelpers {
    public class BoxColliderGizmo : MonoBehaviour
    {
        [SerializeField] private Color _color = Color.green;
        [SerializeField] private bool _alwaysEnabled = true;

        private BoxCollider _collider;

        private BoxCollider Collider { 
            get
            {
                if (_collider == null)
                    _collider = GetComponent<BoxCollider>();
                return _collider;
            } 
        }

        private void OnDrawGizmos()
        {
            if (_alwaysEnabled)
            {
                DrawBox();
            }   
        }

        private void OnDrawGizmosSelected()
        {
            if (!_alwaysEnabled)
            {
                DrawBox();
            }
        }

        private void DrawBox()
        {
            Gizmos.color = _color;
            Vector3 upCenter = transform.position + transform.rotation * (Collider.center + Vector3.up * Collider.size.y * 0.5f);
            Vector3 downCenter = transform.position + transform.rotation * (Collider.center - Vector3.up * Collider.size.y * 0.5f);
            Vector3[] upSquare = GetSquare(upCenter);
            Vector3[] downSquare = GetSquare(downCenter);

            Gizmos.DrawLine(upSquare[0], upSquare[1]);
            Gizmos.DrawLine(upSquare[1], upSquare[2]);
            Gizmos.DrawLine(upSquare[2], upSquare[3]);
            Gizmos.DrawLine(upSquare[3], upSquare[0]);

            Gizmos.DrawLine(downSquare[0], downSquare[1]);
            Gizmos.DrawLine(downSquare[1], downSquare[2]);
            Gizmos.DrawLine(downSquare[2], downSquare[3]);
            Gizmos.DrawLine(downSquare[3], downSquare[0]);

            Gizmos.DrawLine(upSquare[0], downSquare[0]);
            Gizmos.DrawLine(upSquare[1], downSquare[1]);
            Gizmos.DrawLine(upSquare[2], downSquare[2]);
            Gizmos.DrawLine(upSquare[3], downSquare[3]);
        }

        private Vector3[] GetSquare(Vector3 center)
        {
            Vector2 extends = new Vector2(Collider.size.x * 0.5f, Collider.size.z * 0.5f);
            Vector3[] points = new Vector3[4];
            points[0] = center + transform.rotation * new Vector3(extends.x, 0, extends.y);
            points[1] = center + transform.rotation * new Vector3(extends.x, 0, -extends.y);
            points[2] = center + transform.rotation * new Vector3(-extends.x, 0, -extends.y);
            points[3] = center + transform.rotation * new Vector3(-extends.x, 0, extends.y);

            return points;
        }
    }
}
