using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Collectables
{
    public class HealthPickup : Collectable
    {
        protected const string COLLECT_PARAMETER = "Collect";

        public delegate void HealthDelegate(HealthPickup caller);
        public static event HealthDelegate HealthCollectedEvent;

        [Header("Health Properties")]
        [SerializeField] private int _value = 1;
        [SerializeField] private float _collectionAnimationDuration = 0.2f;
        [SerializeField] private Animator _animator;

        public int Value => _value;
        public override float CollectionDuration => _collectionAnimationDuration;


        public override void Collect()
        {
            base.Collect();

            _animator.SetTrigger(COLLECT_PARAMETER);
        }

        protected override void InvokeCollectionEvent()
        {
            HealthCollectedEvent?.Invoke(this);
        }
    }
}