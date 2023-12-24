using System.Collections;
using UnityEngine;
using Gameplay.Klonoa;
using UnityEngine.SceneManagement;
using Sounds;
using Sirenix.OdinInspector;
using Gameplay.Collectables;

namespace Gameplay.Controller
{
    public class GameplayController : MonoBehaviour
    {
        private const int MAX_HEALTH = 6;

        [SerializeField] private KlonoaBehaviour _klonoa;
        [SerializeField] private ResourcesController _resourcesController;
        [SerializeField] private MusicPlayer _music;
        [Header("Settings")]
        [SerializeField] private float _restartTime = 5f;
        [SerializeField] private int _totalStone = 75;

        private void Awake()
        {
            _klonoa.DeathEvent += OnKlonoaDeath;
            _resourcesController.StartLevel(MAX_HEALTH, _totalStone, new bool[6]);
        }

        private void OnKlonoaDeath()
        {
            StartCoroutine(DeathCoroutine());
        }

        private IEnumerator DeathCoroutine()
        {
            _music.StopMusic();
            yield return new WaitForSeconds(_restartTime);
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
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
