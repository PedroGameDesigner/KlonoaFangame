using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using System;
using PlatformerRails;
using Gameplay.Rails;
using Gameplay.Klonoa;

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
        [SerializeField] private float _followPathTime = 1.5f;
        [SerializeField] private float _freeFlyTime = 6;
        [SerializeField] private float _maxGroundDistance;
        [SerializeField] private float _groundCheckLength;

        private BoxCollider _collider;
        private TranslatorOnRails _mover;

        private readonly Vector3 _rayDirection = Vector3.up;
        private Vector3 _lastPosition;
        protected Vector3 _speed;
        protected float _flyDirection;
        protected Vector3 _velocity;
        protected CollisionData _collisionData;

        public Vector3 Position => transform.position;
        public Vector3 ColliderSize => _collider.size;
        public Vector3 BaseSize { get; private set; }
        public float RegrowSpeed { get; private set; }
        public bool FollowPath 
        {
            get => _mover.enabled;
            set
            {
                _mover.Velocity = Vector3.zero;
                _velocity = Vector3.zero;
                _mover.enabled = value;
            } 
        }
        public float FollowPathTime => _followPathTime;
        public float FreeFlyTime => _freeFlyTime;
        public float FlySpeed => _flySpeed;

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
            _mover = GetComponent<TranslatorOnRails>();
            _collisionData = new CollisionData(_maxGroundDistance, _groundCheckLength, _groundLayer);

            BaseSize = _collider.size;
            RegrowSpeed = BaseSize.y / _fullRegrowTime;
            _lastPosition = transform.position;
        }

        private void Update() 
        { 
            _lastPosition = Position;
        }

        private void FixedUpdate()
        {
            _collisionData.CheckGround(transform);
            UpdateSlopeClimb(Time.fixedDeltaTime);
            UpdateFreeMovement();
        }

        private void UpdateFreeMovement()
        {
            if (!FollowPath)
            {
                Vector3 translation = _velocity * Time.fixedDeltaTime;
                transform.Translate(translation, Space.World);
            }
        }

        private void UpdateSlopeClimb(float deltaTime)
        {
            if (_collisionData.Grounded)
            {
                float velocityY = 
                    (_collisionData.MaxGroundDistance - _collisionData.GroundDistance) / deltaTime; //ths results for smooth move on slopes                

                if (FollowPath)
                {
                    Vector3 velocity = _mover.Velocity;
                    velocity.y = velocityY;
                    _mover.Velocity = velocity;
                }
                else
                    _velocity.y = velocityY;
            }
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

        public void Velocity(float speed)
        {
            if (FollowPath)
            {
                _mover.Velocity = Vector3.forward * _flyDirection * _flySpeed;
            }
            else
            {
                _velocity = transform.forward * _flyDirection * _flySpeed;
            }
        }

        public void AssignHolder(Transform holder)
        {
            transform.parent = holder.transform;
        }

        public void Throw(float direction)
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
