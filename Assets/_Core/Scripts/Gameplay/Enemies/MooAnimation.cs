using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class MooAnimation : MonoBehaviour
    {
        private const string WALKING_ANIMATION = "Walking";
        private const string WALKING_SPEED = "WalkSpeed";

        [SerializeField] private MooBehaviour _behaviour;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _defaultSpeed = 1.5f;

        private void Awake()
        {
            _behaviour.StateChangeEvent += OnStateChange;
        }

        private void Start()
        {
            OnStateChange();
        }

        private void OnStateChange()
        {
            _renderer.flipX = _behaviour.Facing < 0 ? true : false;
            _animator.SetBool(WALKING_ANIMATION, _behaviour.IsWalking);
            _animator.SetFloat(WALKING_SPEED, _behaviour.Speed / _defaultSpeed);
        }
    }
}
