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

        [SerializeField] private int _value = 1;
        [Space]
        [SerializeField] SpriteRenderer _renderer = null;
        [SerializeField] ParticleSystem _collectParticles = null;
        [Space]
        [SerializeField] AudioClip _collectedSound = null;
        [SerializeField] AudioSource _audioSource = null;
        [Space]
        [SerializeField] private float _animationDuration = 1;
        [SerializeField] private float _rotationSpeed = 1;

        public int Value => _value;
        public float CollectionDuration => _collectParticles.main.duration;

        protected override void InvokeCollectionEvent()
        {
            StoneCollectedEvent?.Invoke(this);
        }

        protected override void StartDestruction()
        {
            _collider.enabled = false;
            _renderer.enabled = false;

            _collectParticles.Play();
            _audioSource.PlayOneShot(_collectedSound);

            Destroy(gameObject, CollectionDuration);            
        }
    }
}