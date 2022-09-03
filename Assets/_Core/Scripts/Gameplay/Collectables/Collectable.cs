using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Collectables
{
    public abstract class Collectable : MonoBehaviour
    {
        [Header("Collectable Properties")]
        [SerializeField] protected SpriteRenderer _renderer = null;
        [Space]
        [SerializeField] protected AudioClip _collectedSound = null;
        [SerializeField] protected AudioSource _audioSource = null;

        protected Collider _collider;

        public abstract float CollectionDuration { get; }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public virtual void Collect()
        {
            InvokeCollectionEvent();

            _collider.enabled = false;

            _audioSource.PlayOneShot(_collectedSound);

            Destroy(gameObject, CollectionDuration);
        }

        protected abstract void InvokeCollectionEvent();
    }
}