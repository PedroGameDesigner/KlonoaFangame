using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Klonoa;
using System;

namespace Gameplay.Mechanics
{
    public class Tornado : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";

        [SerializeField] private float _jumpHeight;

        private Collider _klonoaCollider;

        public event Action BounceEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (_klonoaCollider == null && other.tag == PLAYER_TAG)
            {
                _klonoaCollider = other;
                var klonoa = _klonoaCollider.GetComponentInParent<KlonoaBehaviour>();
                ImpulseKlonoa(klonoa);
            }
        }

        private void ImpulseKlonoa(KlonoaBehaviour klonoa)
        {
            if (klonoa == null) return;

            float impulse = CalculateImpulse(klonoa.Definition.Gravity);
            
            klonoa.transform.position = transform.position;
            klonoa.ReturnToNormalState();
            klonoa.StartJumpAction(impulse, true, false);
            BounceEvent?.Invoke();
        }

        private float CalculateImpulse(float gravity)
        {
            return Mathf.Sqrt(2 * _jumpHeight * gravity);
        }

        private void OnTriggerExit(Collider other)
        {
            if (_klonoaCollider != null && other.transform == _klonoaCollider.transform)
            {
                _klonoaCollider = null;
            }
        }
    }
}
