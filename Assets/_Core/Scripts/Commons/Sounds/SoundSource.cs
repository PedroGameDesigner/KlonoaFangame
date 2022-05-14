using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    public class SoundSource : MonoBehaviour
    {
        private AudioSource _audioSource;

        private InstanceClip _instanceClip = null;
        private int _lastLoopCount;
        private float _totalTime;

        public float TotalTime => _totalTime;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            _totalTime += Time.deltaTime;
            if (_instanceClip == null) return;

            _instanceClip.PassTime(Time.deltaTime);

            if (_instanceClip.LoopCount > _lastLoopCount)
            {
                if (_instanceClip.Finished)
                {
                    FinishSound();
                }
                else
                {
                    StartSound();
                    _lastLoopCount = _instanceClip.LoopCount;
                }
            }
        }

        public void PlaySound(SoundClip _clip)
        {
            FinishSound();
            _instanceClip = new InstanceClip(_clip);
            _lastLoopCount = 0;
            _totalTime = 0;
            StartSound();
        }

        public void Stop()
        {
            _audioSource.Stop();
            FinishSound();
        }

        private void StartSound()
        {
            Debug.Log(transform.name + " Play: " + _instanceClip.Clip.name);
            if (_instanceClip == null) return;
            _audioSource.PlayOneShot(_instanceClip.Clip);
        }

        private void FinishSound()
        {
            if (_instanceClip == null) return;
            _instanceClip = null;
        }

        sealed class InstanceClip
        {
            private readonly SoundClip _soundClip;

            private float _timer = 0;
            private int _loopCounter = 0;

            public AudioClip Clip => _soundClip.AudioClip;
            public int LoopCount => _loopCounter;
            private bool FirstLoop => _loopCounter < 1;
            private bool ExceedLoopCount => _loopCounter >= _soundClip.LoopCount;
            public bool Finished => !_soundClip.InfiniteLoops &&
                                        ((!_soundClip.Loop && !FirstLoop) ||
                                        ExceedLoopCount);

            public InstanceClip(SoundClip soundClip)
            {
                _soundClip = soundClip;
            }

            public void PassTime(float deltaTime)
            {
                _timer += deltaTime;

                if (_timer >= _soundClip.LoopLength)
                {
                    _timer = 0;
                    _loopCounter += 1;
                }
            }
        }
    }

}