using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using System;

namespace Gameplay.Enemies
{
    public class EnemyBall : MonoBehaviour
    {
        private const int WIDTH_POINTS_COUNT = 3;
        private const int DEPTH_POINTS_COUNT = 3;
        private readonly float RAY_EXTRA_LENGTH = 0.1f;
        private readonly float SKIN_SIZE = 0.05f;

        [SerializeField] private LayerMask _groundLayer = 0;
        [SerializeField] private LayerMask _enemyLayer = 0;
        [SerializeField] private float _fullRegrowTime = 0;

        private BoxCollider _collider;
        private Vector3 _baseSize;

        private readonly Vector3 _rayDirection = Vector3.up;
        private float _regrowSpeed = 0;
        private Vector3 _lastPosition;
        Vector3 _speed;

        private Vector3 InnerColliderSize => _collider.size - Vector3.one * SKIN_SIZE;
        private Vector3 RaysOrigin => ColliderCenter + Vector3.down * _collider.size.y * 0.5f +
                RotateVector(new Vector3(-InnerColliderSize.x, 0, -InnerColliderSize.z) * 0.5f);
        private float WidthSegment => InnerColliderSize.x / (WIDTH_POINTS_COUNT - 1);
        private float DepthSegment => InnerColliderSize.z / (DEPTH_POINTS_COUNT - 1);
        private float RayLength => _baseSize.y + RAY_EXTRA_LENGTH;
        private Vector3 ColliderCenter => transform.position + _collider.center;

        public event Action DestroyEvent;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _baseSize = _collider.size;
            _regrowSpeed = _baseSize.y / _fullRegrowTime;
            _lastPosition = transform.position;
        }

        public void AssignHolder(Transform holder)
        {
            transform.parent = holder.transform;
        }

        private void LateUpdate()
        {
            CalculateSize();
            DetectCollision();
            _lastPosition = transform.position;
        }

        private void CalculateSize() { 
            float collisionDistance = CheckCeilDistance();

            float newHeight = collisionDistance - RAY_EXTRA_LENGTH;
            Vector3 size = _collider.size;

            if (newHeight < size.y)
            {
                ReduceColliderSize(newHeight, size);
            }
            else
            {
                RegrowColliderSize(newHeight, size);
            }
        }

    public float CheckCeilDistance()
        {
            RaycastHit info;
            float collisionDistance = float.PositiveInfinity;
            for (int i = 0; i < WIDTH_POINTS_COUNT; i++)
            {
                for (int j = 0; j < DEPTH_POINTS_COUNT; j++)
                {
                    bool hit = Physics.Raycast(GenerateRayOrigin(i, j), _rayDirection, out info, RayLength, _groundLayer);
                    if (hit)
                        collisionDistance = Mathf.Min(info.distance, collisionDistance);
                }
            }
            return collisionDistance;
        }

        private void ReduceColliderSize(float newHeight, Vector3 size)
        {
            _collider.center = Vector3.down * (_baseSize.y - newHeight) * 0.5f;
            size.y = newHeight;
            _collider.size = size;
        }

        private void RegrowColliderSize(float newHeight, Vector3 size)
        {
            float sizeDiference = Mathf.Min(newHeight, _baseSize.y) - size.y;
            float regrowAmount = Mathf.Min(sizeDiference, _regrowSpeed * Time.deltaTime);
            size.y = size.y + regrowAmount;
            _collider.size = size;
            _collider.center = Vector3.down * (_baseSize.y - size.y) * 0.5f;
        }

        private Vector3 GenerateRayOrigin(int xIndex, int zIndex)
        {
            return RaysOrigin + RotateVector(
                Vector3.right * (WidthSegment * xIndex) +
                Vector3.forward * (DepthSegment * zIndex));
        }

        private Vector3 RotateVector(Vector3 vector)
        {
            return transform.rotation * vector;
        }

        private void DetectCollision()
        {
            _speed = transform.position - _lastPosition;
            RaycastHit[] results;
            _collider.Cast(_speed.normalized, _enemyLayer, out results, _speed.magnitude);
            if (results.Length > 0)
            {
                EnemyBehaviour enemy = results[0].collider.GetComponent<EnemyBehaviour>();
                if (enemy != null)
                {
                    enemy.Kill();
                    DestroySelf();
                }
            }
        }

        private void DestroySelf()
        {
            DestroyEvent?.Invoke();
            transform.parent = null;
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            if (_collider == null) return;

            Gizmos.color = Color.blue;
            for (int i = 0; i < WIDTH_POINTS_COUNT; i++)
            {
                for (int j = 0; j < DEPTH_POINTS_COUNT; j++)
                {
                    if (i == 0 && j == 0) Gizmos.color = Color.cyan;
                    else Gizmos.color = Color.blue;
                    Vector3 origin = GenerateRayOrigin(i, j);
                    Gizmos.DrawSphere(origin, 0.025f);
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(origin, _rayDirection * RayLength);
                }
            }

            Gizmos.color = Color.red;
            Gizmos.DrawRay(_collider.center, _speed.normalized);

        }
    }
}