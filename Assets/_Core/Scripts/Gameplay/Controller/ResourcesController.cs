using GameControl;
using Gameplay.Collectables;
using Gameplay.Klonoa;
using SaveSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Controller
{
    public class ResourcesController : MonoBehaviour
    {
        public int Health { get; private set; }
        public int Stones { get; private set; }
        public bool MoonShard { get; private set; }
        public bool DarkMoonShard { get; private set; }

        public int MaxHealth { get; private set; }
        public int TotalStones { get; private set; }

        private KlonoaBehaviour _klonoa;
        Dictionary<int, DreamStone> dreamstonesDic = new Dictionary<int, DreamStone>();


        public event Action HealthChangeEvent;
        public event Action StonesChangeEvent;

        public void Configure(KlonoaBehaviour klonoa, List<int> collectedStones)
        {
            _klonoa = klonoa;
            _klonoa.DamageEvent += OnKlonoaDamage;
            _klonoa.DeathEvent += OnKlonoaDeath;
            DreamStone.StoneCollectedEvent += OnStoneCollected;
            HealthPickup.HealthCollectedEvent += OnHealthCollected;

            var dreamstones = FindObjectsByType<DreamStone>(FindObjectsSortMode.InstanceID);
            dreamstonesDic.Clear();
            for (int i = 0; i < dreamstones.Length; i++) 
            {
                dreamstonesDic[dreamstones[i].Index] = dreamstones[i];
                if (collectedStones.Contains(dreamstones[i].Index))
                    dreamstones[i].Disable();                
            }
        }

        public void StartLevel(int maxHealth, int totalStones, bool moonShard, bool darkMoonShard)
        {
            MaxHealth = maxHealth;
            TotalStones = totalStones;
            MoonShard = moonShard;
            DarkMoonShard = darkMoonShard;

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
            GameController.LastLevelVisit.collectedDreamstones.Add(stone.Index);
                
        }

        private void OnHealthCollected(HealthPickup healthPickup)
        {
            int newHealth = Health + Mathf.Min(MaxHealth, healthPickup.Value);
            if (newHealth != Health) 
            {
                Health = newHealth;
                StonesChangeEvent?.Invoke();
            }
        }
    }
}