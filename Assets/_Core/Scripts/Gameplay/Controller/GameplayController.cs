using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Klonoa;
using UnityEngine.SceneManagement;
using Sounds;

namespace Gameplay.Controller
{
    public class GameplayController : MonoBehaviour
    {
        private const int MAX_HEALTH = 6;
        private const int TOTAL_STONES = 75;

        [SerializeField] private KlonoaBehaviour _klonoa;
        [SerializeField] private ResourcesController _resourcesController;
        [SerializeField] private MusicPlayer _music;
        [SerializeField] private float _restartTime = 5f;

        private void Awake()
        {
            _klonoa.DeathEvent += OnKlonoaDeath;
            _resourcesController.StartLevel(MAX_HEALTH, TOTAL_STONES, new bool[6]);
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
    }
}
