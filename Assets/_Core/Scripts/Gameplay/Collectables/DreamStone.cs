using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Collectables
{
    public class DreamStone : Collectable
    {
        public delegate void StoneDelegate(DreamStone caller);
        public static event StoneDelegate StoneCollectedEvent;

        [SerializeField] private int _value = 1;

        public int Value => _value;

        protected override void InvokeCollectionEvent()
        {
            StoneCollectedEvent?.Invoke(this);
        }
    }
}