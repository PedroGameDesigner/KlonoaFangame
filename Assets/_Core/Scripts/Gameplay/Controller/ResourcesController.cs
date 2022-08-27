using Gameplay.Collectables;
using Gameplay.Klonoa;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Controller
{
    public class ResourcesController : MonoBehaviour
    {
        [SerializeField] private KlonoaBehaviour _klonoa;

        public int Health { get; private set; }
        public int Stones { get; private set; }
        public bool[] Shards { get; private set; }

        public int MaxHealth { get; private set; }
        public int TotalStones { get; private set; }

        public event Action HealthChangeEvent;
        public event Action StonesChangeEvent;

        private void Awake()
        {
            _klonoa.DamageEvent += OnKlonoaDamage;
            _klonoa.DeathEvent += OnKlonoaDeath;
            DreamStone.StoneCollectedEvent += OnStoneCollected;
        }

        public void StartLevel(int maxHealth, int totalStones, bool[] shards)
        {
            MaxHealth = maxHealth;
            TotalStones = totalStones;
            Shards = shards;

            Health = MaxHealth;
            Stones = 0;
        }

        private void OnKlonoaDamage(int damage)
        {
            Health -= damage;
            HealthChangeEvent?.Invoke();

            if (Health <= 0)
            {
                _klonoa.Death();
            }
        }

        private void OnKlonoaDeath()
        {
            if (Health >= 0)
            {
                Health = 0;
                HealthChangeEvent?.Invoke();
            }
        }

        private void OnStoneCollected(DreamStone stone)
        {
            Stones += stone.Value;
            StonesChangeEvent?.Invoke();
        }
    }
}