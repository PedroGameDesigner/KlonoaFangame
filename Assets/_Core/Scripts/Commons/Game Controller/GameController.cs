using Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameControl
{
    public class GameController : MonoBehaviour
    {
        private static GameController instance;

        private MusicPlayer musicPlayer;
        private SceneStartType sceneStartType;

        public static SceneStartType CurrentSceneStartType => instance.sceneStartType;

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
            instance.musicPlayer.StopMusic();
        }

        public static void StartScene(Scene scene)
        {
            instance.sceneStartType = SceneStartType.Restart;
            SceneManager.LoadScene(scene.buildIndex);
        }

        public static void RestartScene()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            instance.sceneStartType = SceneStartType.Restart;
            SceneManager.LoadScene(activeScene.buildIndex);
        }

        public enum SceneStartType { Start = 0, Restart = 1}
    }
}
