using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour
{
    [SerializeField] private Vector3 _originPosition;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _speed;

    private Transform[] _clouds;
    private float[] _distancesToEnd;
    private Vector3 _direction;

    // Start is called before the first frame update
    private void Awake()
    {
        _clouds = new Transform[transform.childCount];
        _distancesToEnd = new float[_clouds.Length];

        for (int i = 0; i < _clouds.Length; i++)
        {
            _clouds[i] = transform.GetChild(i);
            SetDistance(i);
        }
    }

    private void Update()
    {
        _direction = (_endPosition - _originPosition).normalized;
        for (int i = 0; i < _clouds.Length; i++)
        {
            UpdateCloudPosition(i, Time.deltaTime);
        }
    }

    private void SetDistance(int index)
    {
        Transform cloud = _clouds[index];

        _distancesToEnd[index] = (transform.position + _endPosition - cloud.position).magnitude;
    }


    private void UpdateCloudPosition(int index, float deltaTime)
    {
        Transform cloud = _clouds[index];

        cloud.Translate(_direction * _speed * deltaTime, Space.World);
        _distancesToEnd[index] -= (_direction * _speed * deltaTime).magnitude;
        if (_distancesToEnd[index] <= 0)
        {
            cloud.position = transform.position + _originPosition + (cloud.position - (_endPosition + transform.position));
            SetDistance(index);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + _originPosition, 0.5f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + _endPosition, 0.25f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + _originPosition, transform.position + _endPosition);
    }
}
