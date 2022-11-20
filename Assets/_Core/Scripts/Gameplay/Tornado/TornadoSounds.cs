using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Mechanics
{
    public class TornadoSounds : MonoBehaviour
    {
        [SerializeField] private Tornado _tornado;
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _bounceSound;

        private void Awake()
        {
            _tornado.BounceEvent += OnBounce;
        }

        public void OnBounce()
        {
            _audioSource.PlayOneShot(_bounceSound);
        }
    }
}