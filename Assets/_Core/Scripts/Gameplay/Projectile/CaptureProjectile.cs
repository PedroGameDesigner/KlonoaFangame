using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Projectile
{
    public class CaptureProjectile : MonoBehaviour
    {
        [SerializeField] private float _reach;
        [SerializeField] private float _advanceTime;
        [SerializeField] private float _returnTime;

        private Vector3 _direction;
        private float _extraSpeed;
        private Transform _origin;
        private float _timer;
        private Vector3 _movingFinal;

        public bool Moving { get; private set; }
        public bool Returning { get; private set; }
        private float MovementSpeed => _reach / _advanceTime + _extraSpeed;

        public event Action MovingFinishEvent;
        public event Action ReturnFinishEvent;

        public void StartMovement(Vector3 direction, float extraSpeed, Transform origin)
        {
            if (Moving || Returning) return;
            Debug.Log("SPEED: " + MovementSpeed);
            gameObject.SetActive(true);
            _extraSpeed = Mathf.Abs(extraSpeed);
            _direction = direction;
            _origin = origin;

            Moving = true;
            _timer = 0;
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.deltaTime;
            UpdateMoving(deltaTime);
            UpdateReturn(deltaTime);
        }

        private void UpdateMoving(float deltaTime)
        {
            if (!Moving) return;
            transform.Translate(_direction * MovementSpeed * deltaTime, Space.World);
            _timer += deltaTime;
            if (_timer > _advanceTime)
            {
                _timer = 0;
                Moving = false;
                _movingFinal = transform.position;
                Returning = true;
                MovingFinishEvent?.Invoke();
            }
        }

        private void UpdateReturn(float deltaTime)
        {
            if (!Returning) return;
            transform.position = Vector3.Lerp(_movingFinal, _origin.position, _timer / _returnTime);
            _timer += deltaTime;
            if (_timer >= _returnTime)
            {
                Moving = false;
                Returning = false;
                Finish();
                ReturnFinishEvent?.Invoke();
            }
        }

        private void Finish()
        {
            Destroy(gameObject);
        }
    }
}
