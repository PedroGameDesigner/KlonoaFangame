using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public abstract class HangeableObject : MonoBehaviour, ICapturable
    {
        protected Transform _hangedObject;

        public bool CanBeCaptured => true;

        public void Capture()
        {
        }

        public abstract void MoveToHangingPosition(float time, Action finishAction);
        public abstract void MoveToJumpPosition(float time, Action finishAction);

        internal void SetHangedObject(Transform hangedObject)
        {
            _hangedObject = hangedObject;
        }
    }
}
