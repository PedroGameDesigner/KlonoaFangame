using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.Events;

public class EntranceTrigger : MonoBehaviour
{
    [SerializeField] LayerMask _detectionLayer;
    [SerializeField] private UnityEvent<EntranceTrigger> _entranceCrossedEvent;

    private bool _hasObjectInside;
    private Plane _plane;
    private bool _planeSide;

    private void Awake()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        _plane = GetColliderPlane(collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_detectionLayer.CheckLayer(other.gameObject.layer))
        {
            _hasObjectInside = true;
            _planeSide = _plane.GetSide(other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_hasObjectInside && _detectionLayer.CheckLayer(other.gameObject.layer))
        {
            bool newSide = _plane.GetSide(other.transform.position);
            _hasObjectInside = false;
            if (newSide != _planeSide)
            {
                Debug.Log("Entrance Crossed");
                _entranceCrossedEvent?.Invoke(this);
            }
        }
    }

    private Plane GetColliderPlane(BoxCollider collider)
    {
        Vector2 extends = new Vector2(collider.size.x * 0.5f, collider.size.y * 0.5f);
        Vector3[] planePoints = new Vector3[3];
        planePoints[0] = transform.rotation * (collider.center + new Vector3(extends.x, extends.y));
        planePoints[1] = transform.rotation * (collider.center + new Vector3(extends.x, -extends.y));
        planePoints[2] = transform.rotation * (collider.center + new Vector3(-extends.x, extends.y));
        return new Plane(
            transform.position + planePoints[0],
            transform.position + planePoints[1],
            transform.position + planePoints[2]);
    }

    private void OnDrawGizmosSelected()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector2 extends = new Vector2(collider.size.x * 0.5f, collider.size.y * 0.5f);
        Vector3[] planePoints = new Vector3[3];
        planePoints[0] = transform.rotation * (collider.center + new Vector3(extends.x, extends.y));
        planePoints[1] = transform.rotation * (collider.center + new Vector3(extends.x, -extends.y));
        planePoints[2] = transform.rotation * (collider.center + new Vector3(-extends.x, extends.y));
        Gizmos.color = (Color.blue);
        Gizmos.DrawSphere(transform.position + planePoints[0], 0.1f);
        Gizmos.DrawSphere(transform.position + planePoints[1], 0.1f);
        Gizmos.DrawSphere(transform.position + planePoints[2], 0.1f);
    }
}
