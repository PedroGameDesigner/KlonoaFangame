using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class KlonoaAnimations : MonoBehaviour
    {
        [SerializeField] private KlonoaBehaviour _behaviour = null;
        [Header("Animator Parameter")]
        [SerializeField] private string _groundedParameter = "Grounded";
        [SerializeField] private string _walkingParameter = "Walking";
        [SerializeField] private string _captureProjectileParameter = "CaptureProjectile";
        [SerializeField] private string _beginHoldingParameter = "BeginHolding";
        [SerializeField] private string _isHoldingParameter = "isHolding";
        [SerializeField] private string _endHoldingParameter = "EndHolding";
        [SerializeField] private string _throwParameter = "Throw";
        [SerializeField] private string _floatParameter = "Floating";
        [SerializeField] private string _ySpeedParameter = "YSpeed";
        [SerializeField] private string _doubleJumpParameter = "DoubleJump";
        [SerializeField] private string _damageParameter = "Damage";
        [SerializeField] private string _invincibleParameter = "Invincible";

        private SpriteRenderer _renderer = null;
        private Animator _animator = null;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            _behaviour.CaptureProjectileEvent += OnCaptureProjectile;
            _behaviour.BeginHoldingEvent += OnBeginHolding;
            _behaviour.EndHoldingEvent += OnEndHolding;
            _behaviour.SideThrowEnemyEvent += OnThrow;
        }

        // Update is called once per frame
        void Update()
        {
            _animator.SetBool(_groundedParameter, _behaviour.IsGrounded);
            _animator.SetBool(_walkingParameter, _behaviour.IsWalking);
            _animator.SetBool(_floatParameter, _behaviour.IsFloating);
            _animator.SetFloat(_ySpeedParameter, _behaviour.EffectiveSpeed.y);
            _animator.SetBool(_doubleJumpParameter, _behaviour.IsInDoubleJump);
            _animator.SetBool(_damageParameter, _behaviour.IsInDamage);
            _animator.SetBool(_invincibleParameter, _behaviour.IsInvincible);
            _renderer.flipX = _behaviour.Facing < 0;
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
            _animator.SetTrigger(_endHoldingParameter);
            _animator.SetBool(_isHoldingParameter, false);
        }

        private void OnThrow()
        {
            _animator.SetTrigger(_throwParameter);
        }
    }
}