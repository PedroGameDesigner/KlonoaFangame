using Sirenix.OdinInspector;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(fileName = "Sound Clip", menuName = "Audio/Sound Clip", order = 1)]
    public class SoundClip : ScriptableObject
    {
        [SerializeField] private AudioClip _audioClip;
        [Space]
        [SerializeField] private bool _loop;
        [SerializeField, ShowIf("Loop")] private int _loopCount;
        [SerializeField, ShowIf("Loop")] private float _betweenLoopsTime;

        public AudioClip AudioClip => _audioClip;
        public bool Loop => _loop;
        public bool InfiniteLoops => Loop && _loopCount <= 0;
        public int LoopCount => _loopCount;
        public float BetweenLoopsTime => _betweenLoopsTime;
        public float LoopLength => AudioClip.length + BetweenLoopsTime;
        
    }
}
