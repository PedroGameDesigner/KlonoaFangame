using SaveSystem;
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
        private static SaveManager save;

        [SerializeField] private SceneConfiguration scenesConfig;
        private MusicPlayer musicPlayer;
        private SceneStartType sceneStartType;
        private int currentLevel;
        private LevelVisit lastLevelVisit = new LevelVisit();

        public static SceneStartType CurrentSceneStartType => instance.sceneStartType;
        public static LevelVisit LastLevelVisit
        {
            get => instance.lastLevelVisit;
            set => instance.lastLevelVisit = value;
        }
        public static SaveManager Save => save;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                musicPlayer = GetComponentInChildren<MusicPlayer>();
                save = GetComponentInChildren<SaveManager>();
                save.Load();
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
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

        public static void GoToFirstLevel()
        {
            instance.sceneStartType = SceneStartType.Start;
            instance.currentLevel = 0;
            SceneManager.LoadScene(instance.scenesConfig.FirstLevel);
        }

        public static void GoToNextLevel()
        {
            instance.sceneStartType = SceneStartType.Start;
            instance.currentLevel++;
            if (!instance.scenesConfig.IsLastLevel(instance.currentLevel))
            {
                SceneManager.LoadScene(instance.scenesConfig.GetLevel(instance.currentLevel));
            }
            else
            {
                SetGameCompleted();
                Save.Save();
                SceneManager.LoadScene(instance.scenesConfig.EndingScene);
            }
        }

        public static void GoToMenuScene()
        {
            SceneManager.LoadScene(instance.scenesConfig.MainMenu);
        }

        public static void RestartScene()
        {
            instance.sceneStartType = SceneStartType.Restart;
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
        }

        private static void SetGameCompleted()
        {
            var data = Save.GetData();
            data.gameCompleted = true;
            Save.UpdateData(data);
        }

        public enum SceneStartType { Start = 0, Restart = 1}

        [System.Serializable]
        public class LevelVisit
        {
            public int checkpointID = -1;
            public List<int> collectedDreamstones = new List<int>();
        }
    }
}
