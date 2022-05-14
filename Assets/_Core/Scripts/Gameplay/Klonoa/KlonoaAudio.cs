using Sirenix.OdinInspector;
using Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class KlonoaAudio : MonoBehaviour
    {
        //Constants
        private const string REFERENCES_GROUP = "References";
        private const string CLIPS_GROUP = "Audio Clips";

        //Attributes
        [FoldoutGroup(REFERENCES_GROUP), SerializeField] private KlonoaBehaviour _behaviour = null;
        [FoldoutGroup(REFERENCES_GROUP), SerializeField] private SoundSource[] _soundSources;

        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _jumpSound;
        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _landingSound;
        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _hitCeilSound;
        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _floatSound;
        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _doubleJumpSound;

        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _shotCaptureSound;
        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _captureeSound;
        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _throwSound;
        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _throwVoiceSound;

        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _damageSound;
        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _deathSound;
        
        private void Awake()
        {
            _behaviour.JumpEvent += () => PlaySound(_jumpSound);
            _behaviour.LandingEvent += () => PlaySound(_landingSound);
            _behaviour.StateChangeEvent += OnStateChange;
        }

        private void OnStateChange()
        {
            StopSound();
            if (_behaviour.IsFloating)
            {
                PlaySound(_floatSound);
            }
        }

        private void PlaySound(SoundClip clip)
        {
            Debug.Log("Play Sound: " + clip.name);
            GetOldestSoundSource().PlaySound(clip);
        }

        private void StopSound()
        {
            foreach(SoundSource source in _soundSources)
            {
                source.Stop();
            }
        }

        private SoundSource GetOldestSoundSource()
        {
            int selectedIndex = 0;
            float lastDuration = 0;
            for (int i = 0; i < _soundSources.Length; i++)
            {
                if (_soundSources[i].TotalTime > lastDuration)
                {
                    lastDuration = _soundSources[i].TotalTime;
                    selectedIndex = i;
                }
            }

            Debug.Log("Source is: " + _soundSources[selectedIndex].name);
            return _soundSources[selectedIndex];
        }
    }
}
