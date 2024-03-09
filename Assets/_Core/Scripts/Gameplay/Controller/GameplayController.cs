using System.Collections;
using UnityEngine;
using Gameplay.Klonoa;
using Sirenix.OdinInspector;
using Gameplay.Collectables;
using GameControl;
using UnityEngine.Playables;
using Cameras;
using Sounds;

namespace Gameplay.Controller
{
    public class GameplayController : MonoBehaviour
    {
        private const int MAX_HEALTH = 6;

        [SerializeField] private KlonoaBehaviour _klonoaPrefab;
        [SerializeField] private Transform _klonoaDefaultSpawn;
        [SerializeField] private CameraTarget _cameraTarget;

        [Header("Sequences")]
        [SerializeField] private PlayableDirector _introSequenceDirector;
        [SerializeField] private PlayableDirector _restartSequenceDirector;
        [SerializeField] private PlayableDirector _deathSequenceDirector;
        [SerializeField] private PlayableDirector _endingSequenceDirector;

        [Header("Settings")]
        [SerializeField] private int _totalStone = 75;
        [SerializeField] private MusicClip _levelMusic;

        private KlonoaBehaviour _klonoa;
        private ResourcesController _resourcesController;

        private void Awake()
        {
            Initialized();
        }

        private void Initialized()
        {
            _klonoa = Instantiate(_klonoaPrefab, _klonoaDefaultSpawn.position, _klonoaDefaultSpawn.rotation);
            _klonoa.DeathEvent += OnKlonoaDeath;

            _resourcesController = GetComponentInChildren<ResourcesController>();
            _resourcesController.Configure(_klonoa);
            _resourcesController.StartLevel(MAX_HEALTH, _totalStone, new bool[6]);

            _cameraTarget.Klonoa = _klonoa;
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
            GameController.GoToMenuScene();
        }


#if UNITY_EDITOR
        [Button("Count Dreamstones")]
        private void CountStones()
        {
            _totalStone = 0;
            DreamStone[] dreamStones = FindObjectsByType<DreamStone>(FindObjectsSortMode.None);
            for (int i = 0; i < dreamStones.Length; i++)
            {
                _totalStone += dreamStones[i].Value;
            }
        }

#endif
    }
}
