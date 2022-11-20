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

        private KlonoaBehaviour _klonoa;

        public event Action BounceEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (_klonoa == null && other.tag == PLAYER_TAG)
            {
                _klonoa = other.GetComponent<KlonoaBehaviour>();
                ImpulseKlonoa(_klonoa);
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
            if (_klonoa != null && other.transform == _klonoa.transform)
            {
                _klonoa = null;
            }
        }
    }
}
