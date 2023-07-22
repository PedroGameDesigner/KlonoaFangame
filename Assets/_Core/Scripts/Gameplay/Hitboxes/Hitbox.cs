using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Hitboxes
{
    public class Hitbox : MonoBehaviour
    {
        private const string LAYER_NAME = "Hitbox";

        [SerializeField] 
        private ReceptorType[] _receptorTypes;
        [SerializeField]
        private bool _limitDetection;

        private Collider _collider = null;
        private List<HitDetector> _detectedObjects = new List<HitDetector>();

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == HitDetector.Layer)
            {
                int typeId = GetReceptorID(other);
                if (typeId >= 0)
                {
                    var detector = other.GetComponent<HitDetector>();
                    if (!_limitDetection && detector != null && !_detectedObjects.Contains(detector))
                    {
                        Vector3 contactPoint = _collider.ClosestPoint(transform.position);
                        Vector3 normal = transform.position - contactPoint;
                        HitData data = new HitData(contactPoint, normal, this);
                        InvokeDetectorByType(detector, data, _receptorTypes[typeId].effect);
                        if (_limitDetection)
                            _detectedObjects.Add(detector);
                    }
                }
            }
        }

        private int GetReceptorID(Collider other)
        {
            for (int i = 0; i < _receptorTypes.Length; i++)
            {
                if (other.gameObject.CompareTag(_receptorTypes[i].tag))
                {
                    return i;
                }
            }

            return -1;
        }

        private void InvokeDetectorByType(HitDetector detector, HitData hitData, Effect effect)
        {
            if (effect == Effect.Damage || effect == Effect.All)
                detector.CallDamageEvent(hitData);
            if (effect == Effect.Death || effect == Effect.All)
                detector.CallDeathEvent(hitData);
            if (effect == Effect.Interaction || effect == Effect.All)
                detector.CallInteractEvent(hitData);
        }

        private void OnValidate()
        {
            var collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.isTrigger = true;
            }

            gameObject.layer = LayerMask.NameToLayer(LAYER_NAME);
        }

        [System.Serializable]
        private struct ReceptorType
        {
            public string tag;
            public Effect effect;
        }

        private enum Effect
        {
            Damage,
            Death,
            Interaction,
            All
        }
    }
}
