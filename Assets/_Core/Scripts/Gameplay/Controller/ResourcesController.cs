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

        private void Awake()
        {
            _klonoa.DamageEvent += OnKlonoaDamage;
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

        private void OnKlonoaDamage()
        {
            
        }

        private void OnStoneCollected(DreamStone stone)
        {
            Stones += stone.Value;
        }
    }
}