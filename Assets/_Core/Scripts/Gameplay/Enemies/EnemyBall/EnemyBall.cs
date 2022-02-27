using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using System;

namespace Gameplay.Enemies.Ball
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
        [SerializeField] private float _flySpeed = 4;
        [SerializeField] private float _maxFlyDistance = 10;

        private BoxCollider _collider;

        private readonly Vector3 _rayDirection = Vector3.up;
        private Vector3 _lastPosition;
        protected Vector3 _speed;
        protected Vector3 _flyDirection;

        public Vector3 Position => transform.position;
        public Vector3 ColliderSize => _collider.size;
        public Vector3 BaseSize { get; private set; }
        public float RegrowSpeed { get; private set; }
        public Vector3 FlyVelocity => _flyDirection * _flySpeed;
        public float MaxFlyDistance => _maxFlyDistance;

        private Vector3 InnerColliderSize => _collider.size - Vector3.one * SKIN_SIZE;
        private Vector3 RaysOrigin => ColliderCenter + Vector3.down * _collider.size.y * 0.5f +
                RotateVector(new Vector3(-InnerColliderSize.x, 0, -InnerColliderSize.z) * 0.5f);
        private float WidthSegment => InnerColliderSize.x / (WIDTH_POINTS_COUNT - 1);
        private float DepthSegment => InnerColliderSize.z / (DEPTH_POINTS_COUNT - 1);
        private float RayLength => BaseSize.y + RAY_EXTRA_LENGTH;
        private Vector3 ColliderCenter => transform.position + _collider.center;

        public event Action DestroyEvent;
        public event Action ThrownEvent;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            BaseSize = _collider.size;
            RegrowSpeed = BaseSize.y / _fullRegrowTime;
            _lastPosition = transform.position;
        }

        private void Update() 
        { 
            _lastPosition = Position;
        }

        private void LateUpdate()
        {
            DetectCollision();
        }

        private void DetectCollision()
        {
            _speed = Position - _lastPosition;
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

        public void AssignHolder(Transform holder)
        {
            transform.parent = holder.transform;
        }

        public void Throw(Vector3 direction)
        {
            transform.parent = null;
            _flyDirection = direction;
            ThrownEvent?.Invoke();
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
            return collisionDistance - RAY_EXTRA_LENGTH;
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

        public void ChangeColliderHeight(float newHeight)
        {
            Vector3 size = ColliderSize;
            _collider.center = Vector3.down * (BaseSize.y - newHeight) * 0.5f;
            size.y = newHeight;
            _collider.size = size;
        }
        
        public void DestroySelf()
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
        }
    }
}
