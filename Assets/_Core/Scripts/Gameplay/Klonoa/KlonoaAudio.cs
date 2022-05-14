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
        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _capturedSound;
        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _throwSound;
        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _throwVoiceSound;

        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _damageSound;
        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _deathSound;

        private SoundSource _forStateSource;
        private bool _inDoubleJump = false;

        private void Awake()
        {
            _behaviour.JumpEvent += () => PlaySound(_jumpSound);
            _behaviour.LandingEvent += () => PlaySound(_landingSound);
            _behaviour.CaptureProjectileEvent += () => PlaySound(_shotCaptureSound);
            _behaviour.BeginHoldingEvent += () => PlaySound(_capturedSound);
            _behaviour.SideThrowEnemyEvent += PlayThrowSounds;
            _behaviour.StateChangeEvent += OnStateChange;
        }

        private void OnStateChange()
        {
            StopForStateSound();

            if (_behaviour.IsFloating)
            {
                PlaySoundForState(_floatSound);
            }
            
            if (_behaviour.IsInDoubleJump)
            {
                PlaySound(_jumpSound);
                _inDoubleJump = true;
            }

            if (_inDoubleJump)
            {
                _inDoubleJump = false;
                PlaySound(_doubleJumpSound);
            }

            if (_behaviour.IsInDamage)
            {
                PlaySound(_damageSound);
            }

            if (_behaviour.IsDead)
            {
                PlaySound(_deathSound);
            }
        }

        private void PlaySound(SoundClip clip)
        {
            GetOldestSoundSource().PlaySound(clip);
        }

        private void PlaySoundForState(SoundClip clip)
        {
            if (_forStateSource != null) _forStateSource.Stop();
            _forStateSource = GetOldestSoundSource();
            _forStateSource.PlaySound(clip);
        }

        private void StopSound()
        {
            foreach(SoundSource source in _soundSources)
            {
                source.Stop();
            }
        }

        private void StopForStateSound()
        {
            if (_forStateSource != null)
            {
                _forStateSource.Stop();
                _forStateSource = null;
            }
        }

        private void PlayThrowSounds()
        {
            PlaySound(_throwSound);
            PlaySound(_throwVoiceSound);
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

            return _soundSources[selectedIndex];
        }
    }
}
