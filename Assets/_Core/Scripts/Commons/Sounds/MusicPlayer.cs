using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private MusicClip _music;
        [SerializeField] private float _fadeTime;
        [SerializeField] private bool _startOnAwake;
        [Space]
        [SerializeField] private AudioSource[] _sources;

        private AudioSourceStatus[] _audioStatus;

        private void Awake()
        {
            InitializeAudioSources();
            if (_startOnAwake) StartMusic();
        }

        private void InitializeAudioSources()
        {
            _audioStatus = new AudioSourceStatus[_sources.Length];
            for (int i = 0; i < _sources.Length; i++)
            {
                _audioStatus[i] = new AudioSourceStatus(_sources[i]);
            }
        }

        public void StartMusic()
        {
            int length = Mathf.Min(_audioStatus.Length, _music.AudioTracks.Length);
            for (int i = 0; i < length; i++)
            {
                StartAudioFadeIn(_audioStatus[i], _music.AudioTracks[i]);
            }
        }

        public void StopMusic()
        {
            for (int i = 0; i < _audioStatus.Length; i++)
            {
                StartAudioFadeOut(_audioStatus[i], _music.AudioTracks[i]);
            }
        }

        private void StartAudioFadeIn(AudioSourceStatus audioStatus, AudioClip track)
        {
            if (audioStatus.CurrentCoroutine != null)
            {
                StopCoroutine(audioStatus.CurrentCoroutine);
            }
            audioStatus.CurrentCoroutine = StartCoroutine(FadeInAudio(audioStatus.AudioSource, track));
        }

        private IEnumerator FadeInAudio(AudioSource source, AudioClip track)
        {
            source.clip = track;
            float increment = (1 - source.volume) / _fadeTime;
            source.Play();
            while(source.volume < 1)
            {
                source.volume += increment * Time.deltaTime;
                source.volume = Mathf.Clamp(source.volume, 0, 1);
                yield return null;
            }
        }

        private void StartAudioFadeOut(AudioSourceStatus audioStatus, AudioClip track)
        {
            if (audioStatus.CurrentCoroutine != null)
            {
                StopCoroutine(audioStatus.CurrentCoroutine);
            }
            audioStatus.CurrentCoroutine = StartCoroutine(FadeOutAudio(audioStatus.AudioSource, track));
        }

        private IEnumerator FadeOutAudio(AudioSource source, AudioClip track)
        {
            source.clip = track;
            float decrement = source.volume / _fadeTime;
            while (source.volume > 0)
            {
                source.volume -= decrement * Time.deltaTime;
                source.volume = Mathf.Clamp(source.volume, 0, 1);
                yield return null;
            }
            source.Stop();
        }


        private class AudioSourceStatus
        {
            public AudioSource AudioSource { get; private set; }
            public Coroutine CurrentCoroutine { get; set; }

            public AudioSourceStatus(AudioSource source)
            {
                AudioSource = source;
            }
        }
    }
}
