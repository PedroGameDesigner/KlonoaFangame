using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(fileName = "Music Clip", menuName = "Audio/Music Clip", order = 1)]
    public class MusicClip : ScriptableObject
    {
        [SerializeField] private AudioClip[] _audioTracks;

        public AudioClip[] AudioTracks => _audioTracks;
    }
}