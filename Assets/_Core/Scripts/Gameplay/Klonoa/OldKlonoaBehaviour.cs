using Gameplay.Enemies;
using Gameplay.Enemies.Ball;
using Gameplay.Projectile;
using PlatformerRails;
using System;
using Extensions;
using UnityEngine;
using Gameplay.Collectables;
using Gameplay.Mechanics;
using CylinderCharacterController;
using Colliders;

namespace Gameplay.Klonoa
{
    public class OldKlonoaBehaviour : MonoBehaviour
    {
        [SerializeField] private KlonoaDefinition _definition;
        [Space]
        [SerializeField] private float _maxGroundDistance = 0.5f;
        [SerializeField] private float _groundCheckLength = 0.05f;
        [SerializeField] private float _maxCeilingDistance = 0.25f;
        [SerializeField] private float _ceilingCheckLength = 0.05f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _collisionLayer;
        [SerializeField] private string _enemyTag = "Enemy";
        [SerializeField] private string _deathPlaneTag = "DeathPlane";
        [SerializeField] private string _collectableTag = "Collectable";
        [SerializeField] private float _jumpTimeMarging = 0.15f;
        [Space]
        [SerializeField] private float _minWalkSpeed = 0.1f;
        [Space]
        [SerializeField] private Transform _captureProjectileOrigin;
        [SerializeField] private Transform _enemyProjectileOrigin;
        [SerializeField] private Transform _feetPosition;
        [Space]
        [SerializeField] private BallHolder _ballHolder;
        [SerializeField] private CharacterOnRails _mover;

        private bool _jumpKeep;
        private bool _jumpActivated;
        private bool _invokeJumpEvent;
        private bool _ignoreGround;
        private bool _invincible;
        private float _invincibleTimer;
        private float _jumpForce;
        private bool _previousGrounded = false;
        private bool _previousTouchingCeiling = false;

        private KlonoaStateMachine _stateMachine;
        private Rigidbody _rigidbody;
        private CylinderCollider _collider;
        private Transform _originalParent;
        private CaptureProjectile _projectile;

        private RaycastHit _damageHit;
        private RaycastHit[] _hits = new RaycastHit[10];
        private float resultsCount;
        private float _airTime = 0;
        private Vector3 point1;
        private Vector3 point2;
        private Vector3 checkDirection;

        public KlonoaDefinition Definition => _definition;
        public CharacterOnRails Mover => _mover;
        private Vector2 MoveDirection { set;  get; }
        public CollisionData CollisionData { get; private set; }
        public bool IsGrounded => CollisionData.Grounded;
        public bool IsTouchingCeiling => CollisionData.TouchingCeiling;
        public bool IsWalking => Mathf.Abs(EffectiveSpeed.z) > _minWalkSpeed && Mathf.Abs(MoveDirection.x) > 0;
        public bool IsFloating => _stateMachine.IsFloatState;
        public bool IsInDoubleJump => _stateMachine.IsDoubleJumpState;
        public bool IsInDamage => _stateMachine.IsDamageState;
        public bool IsDead => _stateMachine.IsDeathState;
        public bool IsHolding => HoldedBall != null;
        public bool IsHanging => HangingObject != null;
        public bool IsInvincible => _invincible;
        public bool IsInNormalState => _stateMachine.IsNormalState;
        public bool CaptureProjectileThrowed => _projectile != null;
        public Vector3 EffectiveSpeed => _mover.Velocity;
        public FaceDirection Facing { get; private set; } = FaceDirection.Right;
        public FaceDirection HorizontalFacing { get; private set; } = FaceDirection.Right;
        public EnemyBall HoldedBall { get; private set; }
        public HangeableObject HangingObject { get; private set; }

        //Events
        public event Action StateChangeEvent;
        public event Action JumpEvent;
        public event Action LandingEvent;
        public event Action TouchCeilingEvent;
        public event Action CaptureProjectileEvent;
        public event Action BeginHoldingEvent;
        public event Action EndHoldingEvent;
        public event Action BeginHangingEvent;
        public event Action EndHangingEvent;
        public event Action SideThrowEnemyEvent;
        public event Action<int> DamageEvent;
        public event Action DeathEvent;

        public event Action JumpInputEvent;
        public event Action JumpKeepInputEvent;
        public event Action JumpReleaseInputEvent;
        public event Action AttackInputEvent;
        public event Action<Vector2> DirectionChangeEvent;

        //Behaviour Methods
        void Awake()
        {
            _originalParent = transform.parent;
            _stateMachine = new KlonoaStateMachine(null);
            _stateMachine.StartMachine();
            _stateMachine.StateChangeEvent += OnStateChange;
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<CylinderCollider>();
            CollisionData = new CollisionData(_maxGroundDistance, _groundCheckLength, 
                _maxCeilingDistance, _ceilingCheckLength, _groundLayer);      
        }
        
        private void Update()
        {
            float deltaTime = Time.deltaTime;
            _stateMachine.Update(deltaTime);
            UpdateInvincibility(deltaTime);
        }

        private void UpdateInvincibility(float deltaTime)
        {
            if (_invincible)
            {
                _invincibleTimer += deltaTime;
                if (_invincibleTimer >= Definition.InvincibilityTime)
                {
                    _invincible = false;
                }
            }
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            Vector3 moveSpeed = _mover.Velocity;
            //To make X value 0 means locate the character just above the rail
            moveSpeed.x = -_mover.Position.x * 5f;
            _mover.Velocity = moveSpeed;
            CollisionData.CheckGround(transform);
            CollisionData.CheckCeiling(transform);

            _stateMachine.FixedUpdate(deltaTime);
            if (_jumpKeep)
                JumpKeepInputEvent?.Invoke();

            JumpAction(deltaTime);
            UpdateFacing();
            CheckCollision(deltaTime);
            CheckLandingEvent();
            CheckCeilingEvent();
            UpdateAirTime(deltaTime);
            _jumpActivated = false;
        }

        private void JumpAction(float deltaTime)
        {
            if (CanJump() && _jumpActivated)
            {
                Vector3 velocity = _mover.Velocity;
                velocity.y = _jumpForce + Mathf.Max(0, (_maxGroundDistance - CollisionData.GroundDistance) / deltaTime);
                _mover.Velocity = velocity;
                _airTime += _jumpTimeMarging * 1.1f;
                if (_invokeJumpEvent) JumpEvent?.Invoke();
            }
        }

        public bool CanJump()
        {
            return (_ignoreGround || IsGrounded || 
                (_mover.Velocity.y <= 0 && _airTime <= _jumpTimeMarging));
        }

        private void UpdateFacing()
        {
            if (IsWalking)
            {
                float direction = Mathf.Sign(_mover.Velocity.z);
                Facing = direction > 0 ? FaceDirection.Right : FaceDirection.Left;
            }
            else if (MoveDirection.y != 0)
            {
                Facing = MoveDirection.y > 0 ? FaceDirection.Front : FaceDirection.Back;
            }

            if (Facing == FaceDirection.Right || Facing == FaceDirection.Left)
            {
                HorizontalFacing = Facing;
            }
        }

        private void CheckCollision(float deltaTime)
        {
            /*checkDirection = -Facing.GetVector();
            point1 = _collider.Points()[0];
            point2 = _collider.Points()[1];
            resultsCount = _collider.Cast(checkDirection, _collisionLayer, out _hits, 0.1f * deltaTime);

            if (resultsCount > 0)
            {
                float nearDistance = float.PositiveInfinity;
                int nearIndex = 0;
                for (int i = 0; i < resultsCount; i++)
                {
                    if (_hits[i].distance < nearDistance)
                    {
                        nearDistance = _hits[i].distance;
                        nearIndex = i;
                    }
                }

                OnHit(_hits[nearIndex]);
            }            */
        }

        private void CheckLandingEvent()
        {
            if (IsGrounded && !_previousGrounded)
            {
                _airTime = 0;
                LandingEvent?.Invoke();
            }
            _previousGrounded = IsGrounded;
        }

        private void CheckCeilingEvent()
        {
            if (IsTouchingCeiling && !_previousTouchingCeiling)
            {
                TouchCeilingEvent?.Invoke();
            }
            _previousTouchingCeiling = IsTouchingCeiling;
        }

        private void UpdateAirTime(float deltaTime)
        {
            if (!IsGrounded)
            {
                _airTime += deltaTime;
            }
        }

        private void LateUpdate()
        {
            _stateMachine.LateUpdate(Time.deltaTime);
        }

        private void OnHit(RaycastHit hit)
        {
            if (hit.collider.CompareTag(_enemyTag) && !_invincible)
                OnDamage(hit);
            else if (hit.collider.CompareTag(_collectableTag))
                OnCollectableDetected(hit);
            else if (hit.collider.CompareTag(_deathPlaneTag))
                Death();
        }

        private void OnDamage(RaycastHit hit)
        {
            _invincibleTimer = 0;
            _stateMachine.ChangeToDamageState(hit);
            DamageEvent?.Invoke(1);
            
            /*else
            {
                _stateMachine.ChangeToDeathState();
                DeathEvent?.Invoke();
            }*/
        }

        public void Death()
        {
            _stateMachine.ChangeToDeathState();
            DeathEvent?.Invoke();
        }

        private void OnCollectableDetected(RaycastHit hit)
        {
            Collectable collectable = hit.collider.GetComponent<Collectable>();
            if (collectable != null)
            {
                collectable.Collect();
            }
        }

        public void ReturnToNormalState()
        {
            _stateMachine.ReturnToNormalState();
        }

        public void StartJumpAction(float jumpForce, bool ignoreGround = false, bool invokeJumpEvent = true)
        {
            _jumpActivated = true;
            _invokeJumpEvent = invokeJumpEvent;
            _jumpForce = jumpForce;
            _ignoreGround = ignoreGround;
        }        

        public CaptureProjectile InstantiateCapture()
        {
            Vector3 throwDirection = transform.rotation * Facing.GetVector();
            _projectile = Instantiate(_definition.CaptureProjectile, _captureProjectileOrigin.position, Quaternion.identity);
            _projectile.StartMovement(throwDirection, _mover.Velocity.z, _captureProjectileOrigin);
            CaptureProjectileEvent?.Invoke();

            return _projectile;
        }
        
        public void HoldObject(HoldableObject captureObject)
        {
            HoldedBall = captureObject.GetHoldedVersion(_ballHolder.transform);
            _ballHolder.SetHoldedBall(HoldedBall);
            _ballHolder.RestoreOriginPosition();
            _ballHolder.MoveFromPosition(captureObject.transform.position, _definition.CaptureRepositionTime);
        }

        public void MoveBeforeDoubleJump(Action finishAction)
        {
            if (IsHolding)
            {
                _ballHolder.MoveToPosition(
                    _feetPosition.position,
                    _definition.DoubleJumpPreparationTime,
                    finishAction);
            }
            else if (IsHanging)
            {
                HangingObject.MoveToJumpPosition(
                    _definition.DoubleJumpPreparationTime,
                    finishAction);
            }
        }

        public void HangFromObject(HangeableObject hangeable)
        {
            transform.parent = hangeable.transform;
            HangingObject = hangeable;
            HangingObject.SetHangedObject(transform);
            BeginHangingEvent?.Invoke();
        }

        public void FinishHanging()
        {
            transform.parent = _originalParent;
            HangingObject.SetHangedObject(null);
            HangingObject = null;
            EndHangingEvent?.Invoke();
        }

        public void ThrowHoldedEnemySideways()
        {
            if (HoldedBall == null) return;

            HoldedBall.transform.position = _enemyProjectileOrigin.position;
            HoldedBall.ThrowSide(Facing.GetVector());
            HoldedBall = null;
            SideThrowEnemyEvent?.Invoke();
        }

        public void ThrowHoldedEnemyDownwards()
        {
            if (HoldedBall == null) return;

            HoldedBall.ThrowDown();
            HoldedBall = null;
        }

        public void InvokeBeginHoldingEvent()
        {
            BeginHoldingEvent?.Invoke();
        }

        public void InvokeEndHoldingEvent()
        {
            EndHoldingEvent?.Invoke();
        }

        public void StartInvincibility(float extraTime)
        {
            _invincible = true;
            _invincibleTimer = 0;
        }

        private void OnStateChange()
        {
            StateChangeEvent?.Invoke();
        }

        //Input Access Methods
        public void SetMoveDirection(Vector2 direction)
        {
            MoveDirection = direction;
            DirectionChangeEvent?.Invoke(MoveDirection);
        }

        public void StartJump()
        {
            JumpInputEvent?.Invoke();
            _jumpKeep = true;
        }

        public void EndJump()
        {
            _jumpKeep = false;
            JumpReleaseInputEvent?.Invoke();
        }

        public void StartAttack()
        {
            AttackInputEvent?.Invoke();
        }

        public void StopAttack()
        {
            //unused, added for completition
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Vector3.zero, Vector3.down * (_maxGroundDistance + _groundCheckLength));
            Gizmos.DrawWireCube(Vector3.down * _maxGroundDistance, Vector3.right / 2 + Vector3.forward / 2);
            Gizmos.matrix = Matrix4x4.identity;
            DrawCollisionGizmos();
        }

        void DrawCollisionGizmos()
        {
            if (_collider == null) return;
            Vector3 displacement = 0.1f * checkDirection * Time.deltaTime;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(point1 + displacement, _collider.Radius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(point2 + displacement, _collider.Radius);
        }
#endif
    }
}

