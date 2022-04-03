using Gameplay.Enemies.Ball;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class BallHolder : MonoBehaviour
    {
        private EnemyBall _holdedBall;
        private Vector3 _originalPosition;

        private float _time = 1;
        private Vector3 _origin = Vector3.zero;
        private Vector3 _destination = Vector3.zero;
        private Action _finishAction;
        
        private bool _moving = false;
        private float _timer = 0;

        private Transform Parent => transform.parent;

        private void Start()
        {
            _originalPosition = transform.localPosition;
        }

        void Update()
        {
            if (_moving)
            {
                _timer += Time.deltaTime;
                float lerpValue = _timer / _time;
                transform.localPosition = Vector3.Lerp(_origin, _destination, lerpValue);
                if (_timer >= _time)
                {
                    _moving = false;
                    transform.localPosition = _destination;
                    _finishAction?.Invoke();
                }
            }
        }

        public void SetHoldedBall(EnemyBall holdedBall)
        {
            _holdedBall = holdedBall;
        }

        public void RestoreOriginPosition()
        {
            transform.localPosition = _originalPosition;
        }

        public void MoveToPosition(Vector3 worldPosition, float time, Action finishAction = null)
        {
            _origin = _moving ? _destination : transform.localPosition;
            _destination = Parent.InverseTransformPoint(worldPosition);
            StartTranslation(time, finishAction);
        }

        public void MoveFromPosition(Vector3 worldPosition, float time, Action finishAction = null)
        {
            _destination = _moving ? _origin : transform.localPosition;
            _origin = Parent.InverseTransformPoint(worldPosition);
            StartTranslation(time, finishAction);
        }

        private void StartTranslation(float time, Action finishAction)
        {
            _moving = true;
            _time = time;
            _timer = 0;
            _finishAction = finishAction;
            _holdedBall.StartTransition();
        }
    }
}
