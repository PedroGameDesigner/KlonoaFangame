using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using SpriteSheets;

namespace Gameplay.Klonoa
{
    public class KlonoaAnimations : MonoBehaviour
    {
        //Constants
        private const string REFERENCES_GROUP = "References";
        private const string PARAMETERS_GROUP = "Animator Parameters State";

        //Attributes
        [FoldoutGroup(REFERENCES_GROUP), SerializeField] private KlonoaBehaviour _behaviour = null;
        [FoldoutGroup(REFERENCES_GROUP), SerializeField] private SpriteSheetController _spriteController;
        [FoldoutGroup(REFERENCES_GROUP), SerializeField] private SpriteSheet _sideSpriteSheet;
        [FoldoutGroup(REFERENCES_GROUP), SerializeField] private SpriteSheet _frontSpriteSheet;
        [FoldoutGroup(REFERENCES_GROUP), SerializeField] private SpriteSheet _backSpriteSheet;

        [FoldoutGroup(PARAMETERS_GROUP), SerializeField] private string _groundedParameter = "Grounded";
        [FoldoutGroup(PARAMETERS_GROUP), SerializeField] private string _walkingParameter = "Walking";
        [FoldoutGroup(PARAMETERS_GROUP), SerializeField] private string _captureProjectileParameter = "CaptureProjectile";
        [FoldoutGroup(PARAMETERS_GROUP), SerializeField] private string _beginHoldingParameter = "BeginHolding";
        [FoldoutGroup(PARAMETERS_GROUP), SerializeField] private string _isHoldingParameter = "isHolding";
        [FoldoutGroup(PARAMETERS_GROUP), SerializeField] private string _endHoldingParameter = "EndHolding";
        [FoldoutGroup(PARAMETERS_GROUP), SerializeField] private string _throwParameter = "Throw";
        [FoldoutGroup(PARAMETERS_GROUP), SerializeField] private string _floatParameter = "Floating";
        [FoldoutGroup(PARAMETERS_GROUP), SerializeField] private string _ySpeedParameter = "YSpeed";
        [FoldoutGroup(PARAMETERS_GROUP), SerializeField] private string _doubleJumpParameter = "DoubleJump";
        [FoldoutGroup(PARAMETERS_GROUP), SerializeField] private string _damageParameter = "Damage";
        [FoldoutGroup(PARAMETERS_GROUP), SerializeField] private string _deathParameter = "Death";
        [FoldoutGroup(PARAMETERS_GROUP), SerializeField] private string _invincibleParameter = "Invincible";

        private SpriteRenderer _renderer = null;
        private Animator _animator = null;
        private FaceDirection _lastFacing = FaceDirection.Right;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _lastFacing = _behaviour.Facing;

            _behaviour.CaptureProjectileEvent += OnCaptureProjectile;
            _behaviour.BeginHoldingEvent += OnBeginHolding;
            _behaviour.EndHoldingEvent += OnEndHolding;
            _behaviour.SideThrowEnemyEvent += OnThrow;
            _behaviour.DeathEvent += OnDeath;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateParameters();
            UpdateFacing(_behaviour.Facing);
        }

        private void UpdateParameters()
        {
            _animator.SetBool(_groundedParameter, _behaviour.IsGrounded);
            _animator.SetBool(_walkingParameter, _behaviour.IsWalking);
            _animator.SetBool(_floatParameter, _behaviour.IsFloating);
            _animator.SetFloat(_ySpeedParameter, _behaviour.EffectiveSpeed.y);
            _animator.SetBool(_doubleJumpParameter, _behaviour.IsInDoubleJump);
            _animator.SetBool(_damageParameter, _behaviour.IsInDamage);
            _animator.SetBool(_invincibleParameter, _behaviour.IsInvincible);
        }

        private void UpdateFacing(FaceDirection facing)
        {
            if (_lastFacing != facing)
            {
                switch (facing)
                {
                    case FaceDirection.Front:
                        _spriteController.ChangeSpriteSheet(_backSpriteSheet);
                        break;
                    case FaceDirection.Back:
                        _spriteController.ChangeSpriteSheet(_frontSpriteSheet);
                        break;
                    default:
                        _spriteController.ChangeSpriteSheet(_sideSpriteSheet);
                        _renderer.flipX = facing == FaceDirection.Left;
                        break;
                }
                _lastFacing = facing;
            }
        }

        private void OnCaptureProjectile()
        {
            _animator.SetTrigger(_captureProjectileParameter);
        }

        private void OnBeginHolding()
        {
            _animator.SetTrigger(_beginHoldingParameter);
            _animator.SetBool(_isHoldingParameter, true);
        }

        private void OnEndHolding()
        {
            Debug.Log("End Holding");
            _animator.SetTrigger(_endHoldingParameter);
            _animator.SetBool(_isHoldingParameter, false);
        }

        private void OnThrow()
        {
            _animator.SetTrigger(_throwParameter);
        }

        private void OnDeath()
        {
            _animator.SetTrigger(_deathParameter);
        }
    }
}