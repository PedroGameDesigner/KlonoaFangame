using Gameplay.Enemies;
using Gameplay.Enemies.Ball;
using Gameplay.Projectile;
using PlatformerRails;
using System;
using Extensions;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class KlonoaBehaviour : MonoBehaviour
    {
        [SerializeField] private KlonoaDefinition _definition;
        [Space]
        [SerializeField] private float _maxGroundDistance = 0.5f;
        [SerializeField] private float _groundCheckLength = 0.05f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private string _enemyTag = "Enemy";
        [SerializeField] private string _deathPlaneTag = "DeathPlane";
        [Space]
        [SerializeField] private float _minWalkSpeed = 0.1f;
        [Space]
        [SerializeField] private Transform _captureProjectileOrigin;
        [SerializeField] private Transform _enemyProjectileOrigin;
        [SerializeField] private Transform _feetPosition;
        [Space]
        [SerializeField] private BallHolder _ballHolder;

        private bool _jumpKeep;
        private bool _jumpActivated;
        private bool _ignoreGround;
        private bool _invincible;
        private float _invincibleTimer;
        private float _jumpForce;
        private float _floatYSpeed;

        KlonoaStateMachine _stateMachine;
        MoverOnRails _mover;
        Rigidbody _rigidbody;
        CapsuleCollider _collider;

        CaptureProjectile _projectile;
        RaycastHit _damageHit;
        RaycastHit[] _hits = new RaycastHit[10];
        float resultsCount;
        int _health;
        Vector3 point1;
        Vector3 point2;
        Vector3 checkDirection;

        public KlonoaDefinition Definition => _definition;
        public int Health => _health;
        private Vector2 MoveDirection { set;  get; }
        public CollisionData CollisionData { get; private set; } 
        public bool IsGrounded => CollisionData.Grounded;
        public bool IsWalking => Mathf.Abs(EffectiveSpeed.z) > _minWalkSpeed && Mathf.Abs(MoveDirection.x) > 0;
        public bool IsFloating => _stateMachine.IsFloatState;
        public bool IsInDoubleJump => _stateMachine.IsDoubleJumpState;
        public bool IsInDamage => _stateMachine.IsDamageState;
        public bool IsInvincible => _invincible;
        public bool CaptureProjectileThrowed => _projectile != null;
        public Vector3 EffectiveSpeed => _mover.Velocity;
        public float Facing { get; private set; } = 1;
        public EnemyBall HoldedBall { get; private set; }

        //Events
        public event Action CaptureProjectileEvent;
        public event Action BeginHoldingEvent;
        public event Action EndHoldingEvent;
        public event Action SideThrowEnemyEvent;
        public event Action DamageEvent;
        public event Action DeathEvent;

        public event Action JumpEvent;
        public event Action JumpKeepEvent;
        public event Action JumpReleaseEvent;
        public event Action AttackEvent;
        public event Action<Vector2> DirectionChangeEvent;

        //Behaviour Methods
        void Awake()
        {
            _stateMachine = new KlonoaStateMachine(this);
            _stateMachine.StartMachine();
            _mover = GetComponent<MoverOnRails>();
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
            CollisionData = new CollisionData(_maxGroundDistance, _groundCheckLength, _groundLayer);
            _health = _definition.MaxHealth;
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
            Vector3 velocity = _mover.Velocity;
            //To make X value 0 means locate the character just above the rail
            velocity.x = -_mover.Position.x * 5f;
            _mover.Velocity = velocity;
            CollisionData.CheckGround(transform);

            _stateMachine.FixedUpdate(deltaTime);
            if (_jumpKeep)
                JumpKeepEvent?.Invoke();

            JumpAction(deltaTime);
            UpdateFacing();
            CheckEnemyCollision(deltaTime);
            _jumpActivated = false;
        }

        private void JumpAction(float deltaTime)
        {
            if ((IsGrounded || _ignoreGround ) && _jumpActivated)
            {
                Vector3 velocity = _mover.Velocity;
                velocity.y = _jumpForce + Mathf.Max(0, (_maxGroundDistance - CollisionData.GroundDistance) / deltaTime);
                Debug.Log("Klonoa: JUMP FORCE: " + velocity.y);
                _mover.Velocity = velocity;
            }
        }

        private void UpdateFacing()
        {
            if (IsWalking) Facing = Mathf.Sign(_mover.Velocity.z);
        }

        private void CheckEnemyCollision(float deltaTime)
        {
            if (!_invincible)
            {
                checkDirection = -transform.forward * Facing;
                point1 = _collider.Points()[0];
                point2 = _collider.Points()[1];
                resultsCount = _collider.Cast(checkDirection, _enemyLayer, out _hits, 0.1f * deltaTime);

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
                }
            }
        }
        
        private void LateUpdate()
        {
            _stateMachine.LateUpdate(Time.deltaTime);
        }

        private void OnHit(RaycastHit hit)
        {
            if (hit.collider.CompareTag(_enemyTag))
                OnDamage(hit);
            else if (hit.collider.CompareTag(_deathPlaneTag))
                Death();
        }

        private void OnDamage(RaycastHit hit)
        {
            _invincibleTimer = 0;
            _health -= 1;
            if (_health > 0)
            {
                _stateMachine.ChangeToDamageState(hit);
                DamageEvent?.Invoke();
            }
            else
            {
                _stateMachine.ChangeToDeathState();
                DeathEvent?.Invoke();
            }
        }

        private void Death()
        {
            _health = 0;
            _stateMachine.ChangeToDeathState();
            DeathEvent?.Invoke();
        }

        public void StartJumpAction(float jumpForce, bool ignoreGround = false)
        {
            _jumpActivated = true;
            _jumpForce = jumpForce;
            _ignoreGround = ignoreGround;
        }        

        public CaptureProjectile InstantiateCapture()
        {
            _projectile = Instantiate(_definition.CaptureProjectile, _captureProjectileOrigin.position, Quaternion.identity);
            _projectile.StartMovement(transform.forward * Facing, _mover.Velocity.z, _captureProjectileOrigin);
            CaptureProjectileEvent?.Invoke();

            return _projectile;
        }
        
        public void HoldEnemy(EnemyBehaviour enemy)
        {
            HoldedBall = enemy.InstantiateBall(_ballHolder.transform, _rigidbody);
            _ballHolder.SetHoldedBall(HoldedBall);
            _ballHolder.RestoreOriginPosition();
            _ballHolder.MoveFromPosition(enemy.transform.position, _definition.CaptureRepositionTime);
        }

        public void MoveBallToFeet(Action finishAction)
        {
            _ballHolder.MoveToPosition(
                _feetPosition.position,
                _definition.DoubleJumpPreparationTime,
                finishAction);
        }

        public void ThrowHoldedEnemySideways()
        {
            if (HoldedBall == null) return;

            HoldedBall.transform.position = _enemyProjectileOrigin.position;
            HoldedBall.ThrowSide(Facing);
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

        //Input Access Methods
        public void SetMoveDirection(Vector2 direction)
        {
            MoveDirection = direction;
            DirectionChangeEvent?.Invoke(MoveDirection);
        }

        public void StartJump()
        {
            JumpEvent?.Invoke();
            _jumpKeep = true;
        }

        public void EndJump()
        {
            _jumpKeep = false;
            JumpReleaseEvent?.Invoke();
        }

        public void StartAttack()
        {
            AttackEvent?.Invoke();
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
            Gizmos.DrawWireSphere(point1 + displacement, _collider.radius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(point2 + displacement, _collider.radius);
        }
#endif
    }
}

