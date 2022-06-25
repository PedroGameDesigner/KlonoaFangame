using Sirenix.OdinInspector;
using Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies.Ball
{
    public class EnemyBallSounds : MonoBehaviour
    {
        //Constants
        private const string REFERENCES_GROUP = "References";
        private const string CLIPS_GROUP = "Audio Clips";

        //Attributes
        [FoldoutGroup(REFERENCES_GROUP), SerializeField] private EnemyBall _behaviour = null;
        [FoldoutGroup(REFERENCES_GROUP), SerializeField] private SoundSource _soundSource;

        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _destroySound;
        [FoldoutGroup(CLIPS_GROUP), SerializeField] private SoundClip _bounceSound;

        private void Awake()
        {
            _behaviour.GrowStateChange += OnGrowStateChange;
            _behaviour.DestroyEvent += () => _soundSource.PlaySound(_destroySound);
        }

        private void OnGrowStateChange(GrowState state)
        {
            if (state == GrowState.Reduce && !_soundSource.IsPlaying)
            {
                _soundSource.PlaySound(_bounceSound);
            }
        }
    }
}
