using Gameplay.Enemies.Ball;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Mechanics
{
    public class Goomi : HangeableObject
    {
        private const string HANG_ANIMATION = "Hanged";

        [SerializeField] private Transform _hangPosition;
        [SerializeField] private Transform _jumpPosition;
        [SerializeField] private Animator _animator;

        private bool _moving;
        private float _moveTime;
        private float _moveTimer;
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private Action _finishAction;

        public override void MoveToHangingPosition(float time, Action finishAction)
        {
            StartMovement(time, _hangPosition.localPosition, finishAction);
            ChangeHangAnimation(true);
        }

        public override void MoveToJumpPosition(float time, Action finishAction)
        {
            StartMovement(time, _jumpPosition.localPosition, finishAction);
            ChangeHangAnimation(false);
        }

        private void StartMovement(float time, Vector3 endPosition, Action finishAction)
        {
            if (_moving) return;
            _moving = true;
            _moveTime = time;
            _moveTimer = 0;
            _startPosition = _hangedObject.localPosition;
            _endPosition = endPosition;
            _finishAction = finishAction;
        }

        private void Update()
        {
            if (_moving) MoveHoldedObject(Time.deltaTime);
        }

        private void MoveHoldedObject(float deltaTime)
        {
            _moveTimer += deltaTime;
            _hangedObject.localPosition =
                Vector3.LerpUnclamped(_startPosition, _endPosition, _moveTimer / _moveTime);

            if (_moveTimer >= _moveTime)
            {
                _moving = false;
                _hangedObject.localPosition = _endPosition;
                _finishAction?.Invoke();
            }
        }

        private void ChangeHangAnimation(bool value)
        {
            _animator.SetBool(HANG_ANIMATION, value);
        }
    }
}
