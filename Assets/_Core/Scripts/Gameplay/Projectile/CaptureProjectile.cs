using System;
using UnityEngine;
using Extensions;
using Gameplay.Enemies;

namespace Gameplay.Projectile
{
    public class CaptureProjectile : MonoBehaviour
    {
        [SerializeField] private LayerMask _collisionMask;
        [SerializeField] private float _reach;
        [SerializeField] private float _advanceTime;
        [SerializeField] private float _returnTime;
        [SerializeField] private float _waitFinalTime;

        private Vector3 _direction;
        private float _extraSpeed;
        private Transform _origin;
        private float _timer;
        private Vector3 _movingFinal;
        private Collider _collider;

        public bool Moving { get; private set; }
        public bool Returning { get; private set; }
        private float MovementSpeed => _reach / _advanceTime + _extraSpeed;

        public event Action MovingFinishEvent;
        public event Action ReturnFinishEvent;
        public event Action<EnemyBehaviour> EnemyCapturedEvent;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

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
                FinishMovement();
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

        private void FinishMovement()
        {
            _timer = 0;
            Moving = false;
            _movingFinal = transform.position;
            Returning = true;
            MovingFinishEvent?.Invoke();
        }

        private void Finish()
        {
            enabled = false;
            _collider.enabled = false;
            Destroy(gameObject, _waitFinalTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_collisionMask.CheckLayer(collision.gameObject.layer))
            {
                EnemyBehaviour enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
                enemy.Kill();
                if (enemy.IsCapturable)
                {
                    EnemyCapturedEvent?.Invoke(enemy);
                    Finish();
                }
                else
                    FinishMovement();
            }
        }
    }
}
