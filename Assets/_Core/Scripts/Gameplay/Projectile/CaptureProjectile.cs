using System;
using UnityEngine;
using Extensions;
using Gameplay.Enemies;

namespace Gameplay.Projectile
{
    public class CaptureProjectile : MonoBehaviour, IMovile
    {
        [SerializeField] private LayerMask _captureLayer;
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
        private Vector3 _velocity;

        public bool Moving { get; private set; }
        public bool Returning { get; private set; }
        public Vector3 Velocity => CalculateVelocity();
        private float MovementSpeed => _reach / _advanceTime + _extraSpeed;

        public event Action MovingFinishEvent;
        public event Action ReturnFinishEvent;
        public delegate void CapturableDelegate(ICapturable capturedObject);
        public event CapturableDelegate CapturedEvent;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void StartMovement(Vector3 direction, float extraSpeed, Transform origin)
        {
            if (Moving || Returning) return;
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

            _velocity = _direction * MovementSpeed;
            transform.Translate(_velocity * deltaTime, Space.World);
            _timer += deltaTime;
            if (_timer > _advanceTime)
            {
                FinishMovement();
            }
        }

        private void UpdateReturn(float deltaTime)
        {
            if (!Returning) return;
            Vector3 lastPosition = transform.position;
            transform.position = Vector3.Lerp(_movingFinal, _origin.position, _timer / _returnTime);
            _velocity = (transform.position - lastPosition) / deltaTime;
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
            if (_captureLayer.CheckLayer(collision.gameObject.layer))
            {
                ICapturable capturable = collision.gameObject.GetComponent<ICapturable>();
                capturable.Capture();

                if (capturable.CanBeCaptured)
                {
                    CapturedEvent?.Invoke(capturable);
                    Finish();
                }
                else
                {
                    FinishMovement();
                }                    
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_captureLayer.CheckLayer(other.gameObject.layer))
            {
                ICapturable capturable = other.gameObject.GetComponent<ICapturable>();
                capturable.Capture();

                if (capturable.CanBeCaptured)
                {
                    CapturedEvent?.Invoke(capturable);
                    Finish();
                }
                else
                {
                    FinishMovement();
                }
            }
        }

        private Vector3 CalculateVelocity()
        {
            float xValue = Mathf.Sqrt(Mathf.Pow(_velocity.x, 2) + Mathf.Pow(_velocity.z, 2));
            return new Vector3(xValue, _velocity.y, 0);
        }
    }
}
