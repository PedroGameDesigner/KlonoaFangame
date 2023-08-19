using Colliders;
using Gameplay.Collectables;
using Gameplay.Enemies.Ball;
using Gameplay.Hitboxes;
using Gameplay.Projectile;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class KlonoaBehaviour : MonoBehaviour
    {
        [SerializeField] private KlonoaDefinition _definition;
        [Space]
        [SerializeField] private float _jumpTimeMarging = 0.15f;
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
        private Transform _originalParent;
        private CaptureProjectile _projectile;

        private float _airTime = 0;
        private Vector3 checkDirection;

        public KlonoaDefinition Definition => _definition;
        public CharacterOnRails Mover => _mover;
        private Vector2 MoveDirection { set; get; }
        public bool IsGrounded => Mover.IsGrounded;
        public bool IsTouchingCeiling => Mover.IsTouchingCeiling;
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
        public event System.Action StateChangeEvent;
        public event System.Action JumpEvent;
        public event System.Action LandingEvent;
        public event System.Action TouchCeilingEvent;
        public event System.Action CaptureProjectileEvent;
        public event System.Action BeginHoldingEvent;
        public event System.Action EndHoldingEvent;
        public event System.Action BeginHangingEvent;
        public event System.Action EndHangingEvent;
        public event System.Action SideThrowEnemyEvent;
        public event System.Action<int> DamageEvent;
        public event System.Action DeathEvent;

        public event System.Action JumpInputEvent;
        public event System.Action JumpKeepInputEvent;
        public event System.Action JumpReleaseInputEvent;
        public event System.Action AttackInputEvent;
        public event System.Action<Vector2> DirectionChangeEvent;

        void Awake()
        {
            _originalParent = transform.parent;
            _stateMachine = new KlonoaStateMachine(this);
            _stateMachine.StartMachine();
            _stateMachine.StateChangeEvent += OnStateChange;
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

            _stateMachine.FixedUpdate(deltaTime);
            if (_jumpKeep)
                JumpKeepInputEvent?.Invoke();

            JumpAction(deltaTime);
            UpdateFacing();
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
                velocity.y = _jumpForce;// + Mathf.Max(0, (_maxGroundDistance - CollisionData.GroundDistance) / deltaTime);
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

        private void OnStateChange()
        {
            StateChangeEvent?.Invoke();
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

        public void Death()
        {
            _stateMachine.ChangeToDeathState();
            DeathEvent?.Invoke();
        }

        public void InvokeBeginHoldingEvent()
        {
            BeginHoldingEvent?.Invoke();
        }

        public void InvokeEndHoldingEvent()
        {
            EndHoldingEvent?.Invoke();
        }

        public CaptureProjectile InstantiateCapture()
        {
            Vector3 throwDirection = transform.rotation * Facing.GetVector();
            _projectile = Instantiate(_definition.CaptureProjectile, _captureProjectileOrigin.position, Quaternion.identity);
            _projectile.StartMovement(throwDirection, _mover.Velocity.z, _captureProjectileOrigin);
            CaptureProjectileEvent?.Invoke();

            return _projectile;
        }

        public void MoveBeforeDoubleJump(System.Action finishAction)
        {
            if (IsHolding)
            {
                Mover.RemoveSubCollider(HoldedBall.Collider);
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

        public void HoldObject(HoldableObject captureObject)
        {
            HoldedBall = captureObject.GetHoldedVersion(_ballHolder.transform);
            Mover.AddSubCollider(HoldedBall.Collider);
            _ballHolder.SetHoldedBall(HoldedBall);
            _ballHolder.RestoreOriginPosition();
            _ballHolder.MoveFromPosition(captureObject.transform.position, _definition.CaptureRepositionTime);
        }

        public void ThrowHoldedEnemySideways()
        {
            if (HoldedBall == null) return;

            Mover.RemoveSubCollider(HoldedBall.Collider);
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

        public void StartInvincibility(float extraTime)
        {
            _invincible = true;
            _invincibleTimer = 0;
        }

        public void OnDamage(HitData hitData)
        {
            if (!_invincible)
            {
                _invincibleTimer = 0;
                _stateMachine.ChangeToDamageState(hitData);
                DamageEvent?.Invoke(1);
            }
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
    }
}

