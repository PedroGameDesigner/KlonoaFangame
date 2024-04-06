using System.Collections;
using UnityEngine;
using Gameplay.Klonoa;
using Sirenix.OdinInspector;
using Gameplay.Collectables;
using GameControl;
using UnityEngine.Playables;
using Cameras;
using Sounds;
using SaveSystem;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
#endif

namespace Gameplay.Controller
{
    public class GameplayController : MonoBehaviour
    {
        private const int MAX_HEALTH = 6;

        [SerializeField] private KlonoaBehaviour _klonoaPrefab;
        [SerializeField] private SpawnController _spawnController;
        [SerializeField] private CameraTarget _cameraTarget;

        [Header("Sequences")]
        [SerializeField] private PlayableDirector _introSequenceDirector;
        [SerializeField] private PlayableDirector _restartSequenceDirector;
        [SerializeField] private PlayableDirector _deathSequenceDirector;
        [SerializeField] private PlayableDirector _endingSequenceDirector;

        [Header("Settings")]
        [SerializeField] private int _levelIndex = 0;
        [SerializeField] private int _totalStone = 75;
        [SerializeField] private MusicClip _levelMusic;

        private KlonoaBehaviour _klonoa;
        private ResourcesController _resourcesController;
        private SaveLevelData _levelData;

        private void Awake()
        {
            Initialized();
        }

        private void Initialized()
        {
            _levelData = GameController.Save.GetData().GetLevelData(_levelIndex);

            _spawnController.Configure(GameController.LastLevelVisit.checkpointID);

            var spawnPoint = GetSpawnPosition();
            _klonoa = Instantiate(_klonoaPrefab, spawnPoint.position, spawnPoint.rotation);
            _klonoa.DeathEvent += OnKlonoaDeath;

            _resourcesController = GetComponentInChildren<ResourcesController>();
            _resourcesController.Configure(_klonoa, GameController.LastLevelVisit.collectedDreamstones);
            var moonShard = _resourcesController.MoonShard;
            var darkMoonShard = _resourcesController.DarkMoonShard;
            _resourcesController.StartLevel(MAX_HEALTH, _totalStone, _levelData.moonShard, _levelData.darkMoonShard);

            _cameraTarget.Klonoa = _klonoa;
        }

        private Transform GetSpawnPosition()
        {
            if (GameController.CurrentSceneStartType == GameController.SceneStartType.Start)
            {
                _spawnController.SetCheckpointID(-1);
                return _spawnController.GetDefaultSpawnPosition();
            }
            else
            {
                _spawnController.DisableCurrentCheckPoint();
                return _spawnController.GetLastCheckPoint();
            }
        }

        private void Start()
        {
            if (GameController.CurrentSceneStartType == GameController.SceneStartType.Start)
                StartIntroSequence();
            else
                RestartIntroSequence();
        }

        private void OnKlonoaDeath()
        {
            StartDeathSequence();
        }

        private void StartIntroSequence()
        {
            GameController.PlayMusic(_levelMusic);
            _introSequenceDirector.Play();            
        }

        private void RestartIntroSequence()
        {
            GameController.PlayMusic(_levelMusic);
            _restartSequenceDirector.Play();
        }

        private void StartDeathSequence()
        {
            GameController.StopMusic();
            _deathSequenceDirector.Play();
        }

        public void EnablePlayerInput(bool value)
        {
            _klonoa.EnableInput = value;
        }

        public void RestartScene()
        {
            GameController.RestartScene();
        }

        public void EndLevel()
        {
            GameController.StopMusic();
            _endingSequenceDirector.Play();
        }

        public void GoNextLevel()
        {
            SaveLevelProgress();
            GameController.GoToNextLevel();
        }

        public void SaveLevelProgress()
        {
            _levelData.levelCompleted = true;
            _levelData.dreamStones = Mathf.Max(_levelData.dreamStones, _resourcesController.Stones);
            _levelData.moonShard = _resourcesController.MoonShard;
            _levelData.darkMoonShard = _resourcesController.DarkMoonShard;

            var saveManager = GameController.Save;
            var data = saveManager.GetData();
            data.levelsData[_levelIndex] = _levelData;
            saveManager.UpdateData(data);
            saveManager.Save();
        }

#if UNITY_EDITOR
        [Button("Count & Index Dreamstones")]
        private void CountStones()
        {
            _totalStone = 0;
            DreamStone[] dreamStones = FindObjectsByType<DreamStone>(FindObjectsSortMode.None);
            for (int i = 0; i < dreamStones.Length; i++)
            {
                dreamStones[i].Index = i;
                _totalStone += dreamStones[i].Value;
                EditorUtility.SetDirty(dreamStones[i]);
            }
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

#endif
    }
}
