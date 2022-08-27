using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Collectables
{
    public abstract class Collectable : MonoBehaviour
    {
        protected Collider _collider;
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public virtual void Collect()
        {
            InvokeCollectionEvent();
            StartDestruction();
        }

        protected abstract void InvokeCollectionEvent();

        protected virtual void StartDestruction()
        {
            _collider.enabled = false;
            Destroy(gameObject);
        }
    }
}