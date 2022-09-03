using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Gameplay.Collectables
{
    public class DreamStone : Collectable
    {
        public delegate void StoneDelegate(DreamStone caller);
        public static event StoneDelegate StoneCollectedEvent;

        [Header("Dreamstone Properties")]
        [SerializeField] private int _value = 1;
        [SerializeField] protected ParticleSystem _collectParticles = null;

        public int Value => _value;
        public override float CollectionDuration => _collectParticles.main.duration;

        public override void Collect()
        {
            base.Collect();
            _renderer.enabled = false;

            _collectParticles.Play();
        }

        protected override void InvokeCollectionEvent()
        {
            StoneCollectedEvent?.Invoke(this);
        }
    }
}