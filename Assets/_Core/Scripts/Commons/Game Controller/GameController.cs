using Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameControl
{
    public class GameController : MonoBehaviour
    {
        private static GameController instance;

        private MusicPlayer musicPlayer;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                musicPlayer = GetComponentInChildren<MusicPlayer>();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static void PlayMusic(MusicClip music)
        {
            instance.musicPlayer.StartMusic(music);
        }

        public static void StopMusic()
        {

        }
    }
}
