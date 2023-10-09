using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class SpawnPortal : MonoBehaviour
    {
        [SerializeField] private List<EnemyBehaviour> _enemies;
        [SerializeField] private float _respawnTime = 5f;
        [SerializeField] private float _repositionTime = 2f;
        [SerializeField] private AnimationCurve _verticalRepositionCurve;
        [SerializeField] private AnimationCurve _repositionCurve;

        private List<SpawnTimer> _spawnTimers = new List<SpawnTimer>();
        private List<SpawnTimer> _finishedTimers = new List<SpawnTimer>();

        private bool HasTimers => _spawnTimers.Count > 0;

        public event Action StartSpawnEvent;
        public event Action EndSpawnEvent;

        private void Awake()
        {
            foreach(var enemy in _enemies)
            {
                enemy.DeathEvent += OnEnemyDeath;
            }
        }

        private void Update()
        {
            if (HasTimers)
            {
                float deltaTime = Time.deltaTime;
                _finishedTimers.Clear();
                foreach (var timer in _spawnTimers)
                {
                    timer.Update(transform.position, deltaTime, _repositionTime, _repositionCurve, _verticalRepositionCurve);
                    if (timer.Finished)
                        _finishedTimers.Add(timer);
                }

                foreach (var timer in _finishedTimers)
                {
                    _spawnTimers.Remove(timer);
                }

                if (!HasTimers)
                {
                    EndSpawnEvent?.Invoke();
                }
            }
        }

        private void OnEnemyDeath(EnemyBehaviour caller)
        {
            caller.transform.position = transform.position;
            if (!HasTimers)
                StartSpawnEvent?.Invoke();

            _spawnTimers.Add(new SpawnTimer(caller, _respawnTime));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            for(int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] != null)
                    Gizmos.DrawLine(transform.position, _enemies[i].transform.position);
                else
                {
                    Gizmos.DrawWireSphere(transform.position, 0.5f);
                    break;
                }
            }
        }

        private class SpawnTimer
        {
            private readonly EnemyBehaviour _enemy;
            private readonly float _time;

            private float _timer = 0;

            public EnemyBehaviour Enemy => _enemy;
            public bool Finished { get; private set; }

            public SpawnTimer(EnemyBehaviour enemy, float time)
            {
                _enemy = enemy;
                _time = time;
            }

            public void Update (Vector3 spawnPosition, float deltaTime, float repositiontime, AnimationCurve repositionCurve, AnimationCurve verticalCurve)
            {
                if (!Finished)
                {
                    _timer += deltaTime;

                    if (_timer >= _time)
                    {
                        _enemy.Respawn(spawnPosition, repositiontime, repositionCurve, verticalCurve);
                        Finished = true;
                    }
                }
            }
        }
    }
}
