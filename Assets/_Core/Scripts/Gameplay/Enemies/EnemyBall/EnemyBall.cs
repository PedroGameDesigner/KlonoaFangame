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
        [SerializeField] private float _destroyDelay = 0.25f;
        [SerializeField] private float _growStateInertia = 0.1f;
        [SerializeField] private float _maxGroundDistance;
        [SerializeField] private float _groundCheckLength;

        private BoxCollider _collider;
        private TranslatorOnRails _mover;

        private readonly Vector3 _rayDirection = Vector3.up;
        private Vector3 _lastPosition;
        protected Vector3 _speed;
        protected Vector3 _flyDirection;
        protected Vector3 _velocity;
        protected CollisionData _collisionData;
        protected GrowState _growState = GrowState.Inert;
        protected float _growStateTimer;

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

        public bool ClimbSlope { get; set; }
        public bool CollisionEnabled { get; set; }
        public float FollowPathTime => _followPathTime;
        public float FreeFlyTime => _freeFlyTime;
        public float FullFlyTime => _followPathTime + _freeFlyTime;
        public float FlySpeed => _flySpeed;
        public float DestroyDelay => _destroyDelay;
        public CollisionType SelectedCollisionType { get; set; } = CollisionType.None;
        public LayerMask CollisionMask
        {
            get
            {
                switch (SelectedCollisionType)
                {
                    case CollisionType.Enemies:
                        return _enemyLayer;
                    case CollisionType.Ground:
                        return _groundLayer;
                    case CollisionType.All:
                        return _enemyLayer | _groundLayer;
                    default:
                        return new LayerMask();
                }
            }
        }

        public Collider Collider => _collider;
        private Vector3 Velocity => FollowPath ? _mover.Velocity : _velocity;
        private Vector3 InnerColliderSize => _collider.size - Vector3.one * SKIN_SIZE;
        private Vector3 RaysOrigin => ColliderCenter + Vector3.down * _collider.size.y * 0.5f +
                RotateVector(new Vector3(-InnerColliderSize.x, 0, -InnerColliderSize.z) * 0.5f);
        private float WidthSegment => InnerColliderSize.x / (WIDTH_POINTS_COUNT - 1);
        private float DepthSegment => InnerColliderSize.z / (DEPTH_POINTS_COUNT - 1);
        private float RayLength => BaseSize.y + RAY_EXTRA_LENGTH;
        private Vector3 ColliderCenter => transform.position + _collider.center;

        public event Action<GrowState> GrowStateChange;
        public event Action DestroyEvent;
        public event Action StartTransitionEvent;
        public event Action TransitionFinishEvent;
        public event Action<Vector3> ThrownEvent;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _mover = GetComponent<TranslatorOnRails>();
            _collisionData = new CollisionData(_maxGroundDistance, _groundCheckLength, 0, 0, _groundLayer);

            BaseSize = _collider.size;
            RegrowSpeed = BaseSize.y / _fullRegrowTime;
            _lastPosition = transform.position;
        }
        
        private void FixedUpdate()
        {
            _lastPosition = Position;
            _collisionData.CheckGround(transform);
            UpdateSlopeClimb(Time.fixedDeltaTime);
            UpdateFreeMovement();
            _growStateTimer += Time.fixedDeltaTime;
        }

        private void UpdateFreeMovement()
        {
            if (!FollowPath)
            {
                Vector3 translation = _velocity * Time.fixedDeltaTime;
                transform.Translate(translation, Space.Self);
            }
        }

        private void UpdateSlopeClimb(float deltaTime)
        {
            if (_collisionData.Grounded && ClimbSlope)
            {
                float velocityY = 
                    (_collisionData.MaxGroundDistance - _collisionData.GroundDistance) / deltaTime; //ths results for smooth move on slopes                

                velocityY = Mathf.Max(0, velocityY);
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
            if (!CollisionEnabled || !_collider.enabled) return;

            _speed = Position - _lastPosition;
            Vector3 speed;
            if (Velocity.magnitude > 0)
                speed = Velocity * Time.deltaTime;
            else
                speed = _speed;

            RaycastHit[] results;
            _collider.Cast(speed.normalized, CollisionMask, out results, speed.magnitude);

            if (results.Length > 0)
            {
                Debug.Log("Enemy hit: " + results[0].transform.name);
                EnemyBehaviour enemy = results[0].collider.GetComponent<EnemyBehaviour>();
                if (enemy != null)
                    enemy.Kill();
                
                DestroySelf();
            }

            if ((_collisionData.Grounded && _flyDirection.y < 0))
            {
                DestroySelf();
            }
        }

        public void SetVelocity(float speed)
        {
            if (FollowPath)
            {
                _mover.Velocity = _flyDirection * speed;
            }
            else
            {
                _velocity = _flyDirection * speed;
            }
        }

        public void AssignHolder(Transform holder)
        {
            transform.parent = holder.transform;
        }

        public void StartTransition()
        {
            StartTransitionEvent?.Invoke();
        }

        public void ThrowSide(Vector3 direction)
        {
            TransitionFinishEvent?.Invoke();
            _lastPosition = transform.position;
            transform.parent = null;
            _flyDirection = direction;
            ThrownEvent?.Invoke(_flyDirection);
        }

        public void ThrowDown()
        {
            TransitionFinishEvent?.Invoke();
            _lastPosition = transform.position;
            transform.parent = null;
            _flyDirection = Vector3.down;
            ThrownEvent?.Invoke(_flyDirection);
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
            float previousSize = size.y;
            _collider.center = Vector3.down * (BaseSize.y - newHeight) * 0.5f;
            size.y = newHeight;
            _collider.size = size;

            if (previousSize > _collider.size.y)
            {
                ChangeGrowState(GrowState.Reduce);
            }
            else if (previousSize < _collider.size.y)
            {
                ChangeGrowState(GrowState.Grow);
            }
            else
            {
                ChangeGrowState(GrowState.Inert);
            }
        }

        private void ChangeGrowState(GrowState newState)
        {
            if (_growState != newState &&
                (newState != GrowState.Inert && _growStateTimer > _growStateInertia))
            {
                _growState = newState;
                GrowStateChange?.Invoke(_growState);
                _growStateTimer = 0;
            }
        }

        public void DestroySelf()
        {
            DestroyEvent?.Invoke();
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

        public enum CollisionType
        {
            None,
            All,
            Ground,
            Enemies
        }
    }

    public enum GrowState
    {
        Inert,
        Grow,
        Reduce
    }
}
