using System.Collections;
using UnityEngine;
using Gameplay.Klonoa;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using Gameplay.Collectables;
using GameControl;
using UnityEngine.Playables;
using Cameras;

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
        [SerializeField] private PlayableDirector _deathSequenceDirector;

        [Header("Settings")]
        [SerializeField] private int _totalStone = 75;

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
            
            StartIntroSequence();
        }

        private void OnKlonoaDeath()
        {
            StartCoroutine(DeathCoroutine());
        }

        private IEnumerator DeathCoroutine()
        {
            GameController.StopMusic();
            yield return null;//new WaitForSeconds(_restartTime);
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
        }

        private void StartIntroSequence()
        {
            _introSequenceDirector.Play();            
        }

        private void OnIntroEnded(PlayableDirector director)
        {
            director.stopped -= OnIntroEnded;
            _klonoa.EnableInput = true;
        }

        private void StartDeathSequence()
        {
            _deathSequenceDirector.Play();
        }

        public void EnablePlayerInput(bool value)
        {
            _klonoa.EnableInput = value;
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
