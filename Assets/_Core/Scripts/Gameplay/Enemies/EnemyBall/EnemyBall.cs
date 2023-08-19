using UnityEngine;
using Extensions;
using System;
using Gameplay.Rails;
using Gameplay.Klonoa;
using Gameplay.Hitboxes;
using CylinderCharacterController;
using Colliders;

namespace Gameplay.Enemies.Ball
{
    public class EnemyBall : MonoBehaviour
    {
        private const int WIDTH_POINTS_COUNT = 3;
        private const int DEPTH_POINTS_COUNT = 3;
        private readonly float RAY_EXTRA_LENGTH = 0.05f;

        [Header("Colliders")]
        [SerializeField] private Hitbox _hitbox = null;
        [SerializeField] private CollisionDetector _collisionDetector = null;

        [Header("Fly Settings")]
        [SerializeField] private float _flySpeed = 4;
        [SerializeField] private float _followPathTime = 1.5f;
        [SerializeField] private float _freeFlyTime = 6;
        [SerializeField] private float _destroyDelay = 0.25f;

        [Header("Resize Settings")]
        [SerializeField] private LayerMask _groundLayer = 0;
        [SerializeField] private float _fullRegrowTime = 0;
        [SerializeField] private float _growStateInertia = 0.1f;
        [SerializeField] private float _maxGroundDistance;
        [SerializeField] private float _groundCheckLength;


        private CylinderCollider _collider;
        private CharacterOnRails _mover;

        protected Vector3 _speed;
        protected Vector3 _flyDirection;
        protected Vector3 _velocity;
        protected CollisionData _collisionData;
        protected GrowState _growState = GrowState.Inert;
        protected float _growStateTimer;

        public Vector3 Position => transform.position;
        public float ColliderHeight => _collider.Height;
        public float BaseHeight { get; private set; }
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
        public bool IsSolid
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }

        public bool CollideWithGround
        {
            get => _collisionDetector.enabled;
            set => _collisionDetector.enabled = value;
        }
        public bool CollideWithEnemy
        {
            get => _hitbox.enabled;
            set => _hitbox.enabled = value;
        }

        public bool ClimbSlope { get; set; }
        public float FollowPathTime => _followPathTime;
        public float FreeFlyTime => _freeFlyTime;
        public float FullFlyTime => _followPathTime + _freeFlyTime;
        public float FlySpeed => _flySpeed;
        public float DestroyDelay => _destroyDelay;

        public CylinderCollider Collider => _collider;
        private Vector3 Velocity => FollowPath ? _mover.Velocity : _velocity;
        private float RayLength => BaseHeight + RAY_EXTRA_LENGTH;
        private Vector3 ColliderCenter => transform.position + _collider.Height * Vector3.up;

        public event Action<GrowState> GrowStateChange;
        public event Action DestroyEvent;
        public event Action StartTransitionEvent;
        public event Action TransitionFinishEvent;
        public event Action<Vector3> ThrownEvent;

        private void Awake()
        {
            _collider = GetComponent<CylinderCollider>();
            _mover = GetComponent<CharacterOnRails>();
            _collisionData = new CollisionData(_maxGroundDistance, _groundCheckLength, 0, 0, _groundLayer);

            BaseHeight = _collider.Height;
            RegrowSpeed = BaseHeight / _fullRegrowTime;
        }
        
        private void FixedUpdate()
        {
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
            if (!FollowPath && _collisionData.Grounded && ClimbSlope)
            {
                float velocityY = 
                    (_collisionData.MaxGroundDistance - _collisionData.GroundDistance) / deltaTime; //ths results for smooth move on slopes                

                velocityY = Mathf.Max(0, velocityY);
                _velocity.y = velocityY;
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
            transform.parent = null;
            _flyDirection = direction;
            ThrownEvent?.Invoke(_flyDirection);
        }

        public void ThrowDown()
        {
            TransitionFinishEvent?.Invoke();
            transform.parent = null;
            _flyDirection = Vector3.down;
            ThrownEvent?.Invoke(_flyDirection);
        }

        public float CheckCeilDistance()
        {
            float collisionDistance = float.PositiveInfinity;
            for (int i = 0; i < WIDTH_POINTS_COUNT; i++)
            {
                for (int j = 0; j < DEPTH_POINTS_COUNT; j++)
                {
                    int count = _collider.CheckVerticalCollision(RayLength, Vector3.down * ColliderHeight);
                    if (count > 0)
                        collisionDistance = Mathf.Min(_collider.GetClosestVerticalHit().distance, collisionDistance);
                }
            }
            return collisionDistance - RAY_EXTRA_LENGTH;
        }        

        public void ChangeColliderHeight(float newHeight)
        {
            float height = ColliderHeight;
            float previousHeight = height;
            height = newHeight;
            _collider.Height = height;
            _collider.GeneratePointPositions();

            if (previousHeight > _collider.Height)
            {
                ChangeGrowState(GrowState.Reduce);
            }
            else if (previousHeight < _collider.Height)
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
