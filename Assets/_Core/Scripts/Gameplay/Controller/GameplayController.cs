using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Klonoa;
using UnityEngine.SceneManagement;

namespace Gameplay.Controller
{

    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private KlonoaBehaviour _klonoa;
        [SerializeField] private float _restartTime = 5f;

        private void Awake()
        {
            _klonoa.DeathEvent += OnKlonoaDeath;
        }

        private void OnKlonoaDeath()
        {
            StartCoroutine(DeathCoroutine());
        }

        private IEnumerator DeathCoroutine()
        {
            yield return new WaitForSeconds(_restartTime);
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
        }
    }
}
