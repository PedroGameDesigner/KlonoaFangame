using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace Menu
{
    public class MenuAmbientSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip _ambientSound;
        [SerializeField] private AudioSource _source;
        [SerializeField] private Vector2 _pitchRange;
        [SerializeField] private Vector2 _delayRange;

        private Coroutine _coroutine;

        private void Start()
        {
            _coroutine = StartCoroutine(AmbientSoundCoroutine());
        }

        private IEnumerator AmbientSoundCoroutine()
        {
            while (true)
            {
                float delay = _ambientSound.length + _delayRange.RandomValue();
                yield return new WaitForSeconds(delay);
                _source.pitch = _pitchRange.RandomValue();
                _source.PlayOneShot(_ambientSound);
            }
        }

        public void StopSound()
        {
            StopCoroutine(_coroutine);
        }
    }
}