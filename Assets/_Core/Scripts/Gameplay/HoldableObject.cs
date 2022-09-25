using Gameplay.Enemies.Ball;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public abstract class HoldableObject : MonoBehaviour, ICapturable
    {
        protected Collider _collider;

        public abstract bool CanBeCaptured { get; }

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public virtual void Capture()
        {
            Enable(false);
        }

        protected void Enable(bool value)
        {
            enabled = value;
            _collider.enabled = value;
        }

        public abstract EnemyBall GetHoldedVersion(Transform holderTransform);
    }
}