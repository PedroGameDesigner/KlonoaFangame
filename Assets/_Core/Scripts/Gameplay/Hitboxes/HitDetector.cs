using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Hitboxes
{
    public class HitDetector : MonoBehaviour
    {
        private const string LAYER_NAME = "HitDetector";

        private static int _layer = -1;
        public static int Layer
        {
            get
            {
                if (_layer < 0)
                    _layer = LayerMask.NameToLayer(LAYER_NAME);
                return _layer;
            }
        }

        [SerializeField]
        private UnityEvent<HitData> _damageHitEvent;

        [SerializeField]
        private UnityEvent<HitData> _deathHitEvent;

        [SerializeField]
        private UnityEvent<HitData> _interactHitEvent;

        public delegate void HitAction();
        private event System.Action<HitData> _damageHitAction;
        private event System.Action<HitData> _deathHitAction;
        private event System.Action<HitData> _interactHitAction;

        //Subscription
        public void SubscribeDamageAction(System.Action<HitData> action)
        {
            _damageHitAction += action;
        }
        public void SubscribeDeathAction(System.Action<HitData> action)
        {
            _deathHitAction += action;
        }
        public void SubscribeInteractionAction(System.Action<HitData> action)
        {
            _interactHitAction += action;
        }

        //Unsubscription
        public void UnsubscribeDamageAction(System.Action<HitData> action)
        {
            _damageHitAction -= action;
        }
        public void UnsubscribeDeathAction(System.Action<HitData> action)
        {
            _deathHitAction -= action;
        }
        public void UnsubscribeInteractionAction(System.Action<HitData> action)
        {
            _interactHitAction -= action;
        }

        //Calls
        public void CallDamageEvent(HitData hitData)
        {
            _damageHitEvent.Invoke(hitData);
            _damageHitAction?.Invoke(hitData);
        }

        public void CallDeathEvent(HitData hitData)
        {
            _deathHitEvent.Invoke(hitData);
            _deathHitAction?.Invoke(hitData);
        }

        public void CallInteractEvent(HitData hitData)
        {
            _interactHitEvent.Invoke(hitData);
            _interactHitAction?.Invoke(hitData);
        }

        private void OnValidate()
        {
            var collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.isTrigger = false;
            }

            var rigidbody = GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.isKinematic = true;
                rigidbody.useGravity = false;
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }

            gameObject.layer = LayerMask.NameToLayer(LAYER_NAME);
        }
    }
}
