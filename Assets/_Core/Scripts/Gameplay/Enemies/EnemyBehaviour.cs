using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Enemies.Ball;
using UnityEngine;

namespace Gameplay.Enemies
{
    public abstract class EnemyBehaviour : HoldableObject, IDamageable
    {
        protected const float RESPAWN_TIME = 0.5f;

        [SerializeField] protected EnemyDefinition _definition = null;
        [SerializeField] protected EnemyBall _ballPrefab = null;

        protected State _state = State.Active;
        protected Vector3 _originPosition;
        protected Vector3 _spawnPosition;
        protected Vector3 _spawnDirection;
        protected AnimationCurve _repositionCurve;
        protected AnimationCurve _repositionVerticalCurve;
        protected float _spawnHeight;
        protected float _spawnTime;
        protected float _gravity;
        protected float _spawnTimer;

        protected EnemyBall SpawnedBall { get; set; }
        protected bool IsActive => _state == State.Active;

        public delegate void EnemyEvent(EnemyBehaviour caller);
        public event EnemyEvent DeathEvent;
        public event Action StateChangeEvent;

        protected override void Awake()
        {
            base.Awake();
            _originPosition = transform.position;
            Debug.Log("_originPosition = " + _originPosition);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            switch (_state)
            {
                case State.Active: UpdateActiveState(deltaTime); return;
                case State.Spawning: UpdateSpawningState(deltaTime); return;
                case State.Disable: return;
            }
        }

        protected virtual void UpdateActiveState(float deltaTime)
        {

        }

        protected virtual void UpdateSpawningState(float deltaTime)
        {
            _spawnTimer += deltaTime;
            float timerNormal = _spawnTimer / _spawnTime;
            transform.position = Vector3.Lerp(_spawnPosition, _originPosition, timerNormal);
            transform.position += Vector3.up * _repositionVerticalCurve.Evaluate(timerNormal);

            if (_spawnTimer >= _spawnTime)
            {
                transform.position = _originPosition;
                ChangeToActiveState();
            }
        }

        protected virtual void ChangeToActiveState()
        {
            _state = State.Active;
            InvokeStateChangeEvent();
        }

        protected virtual void ChangeToSpawningState(Vector3 spawnPosition, float spawnTime, AnimationCurve repositionCurve, AnimationCurve verticalCurve)
        {
            _spawnTimer = 0;
            _spawnPosition = spawnPosition;
            _spawnTime = spawnTime;
            _repositionCurve = repositionCurve;
            _repositionVerticalCurve = verticalCurve;
            _state = State.Spawning;
            InvokeStateChangeEvent();
        }

        protected virtual void ChangeToDisableState()
        {
            _state = State.Disable;
            InvokeStateChangeEvent();
        }

        protected void InvokeStateChangeEvent()
        {
            StateChangeEvent?.Invoke();
            Debug.Log($"Enemy {name} change state: {_state}"); ;
        }

        protected void ChangeToDisable()
        {
            _state = State.Disable;
            StateChangeEvent?.Invoke();
        }

        public override EnemyBall GetHoldedVersion(Transform holderTransform)
        {
            SpawnedBall = Instantiate(_ballPrefab, holderTransform.position, holderTransform.rotation);
            SpawnedBall.AssignHolder(holderTransform);
            SpawnedBall.DestroyEvent += OnBallDestroyed;
            return SpawnedBall;
        }

        public override void Capture()
        {
            gameObject.SetActive(false);
        }

        public virtual void DoDamage()
        {
            Kill();
        }

        public virtual void Respawn(Vector3 spawnPosition, float spawnTime, AnimationCurve repositionCurve, AnimationCurve verticalCurve)
        {
            gameObject.SetActive(true);
            ChangeToSpawningState(spawnPosition, spawnTime, repositionCurve, verticalCurve);
        }

        protected virtual void Kill()
        {
            DeathEvent?.Invoke(this);
            gameObject.SetActive(false);
        }

        protected void OnBallDestroyed()
        {
            Kill();
        }

        protected enum State
        {
            Disable,
            Spawning,
            Active
        }
    }
}
