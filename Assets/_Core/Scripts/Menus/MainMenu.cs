using GameControl;
using Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;
        [Space]
        [SerializeField] private MusicPlayer _musicPlayer;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _selectSound;
        [SerializeField] private AudioClip _playSound;
        [SerializeField] private AudioClip _exitSound;

        private bool _actionSelected;
        private bool _firstChange;

        public void ExitGame()
        {
            if (_actionSelected) return;
            _actionSelected = true;
            _audioSource.PlayOneShot(_exitSound);
            StopEvents();
            StartCoroutine(DelayedQuit());
        }

        private IEnumerator DelayedQuit()
        {
            yield return new WaitForSeconds(_exitSound.length);
            Application.Quit();
        }

        public void PlayLevel()
        {
            if (_actionSelected) return;
            _actionSelected = true;
            _audioSource.PlayOneShot(_playSound);
            _musicPlayer.StopMusic();
            StopEvents();
            StartCoroutine(DelayedPlayLevel());
        }

        private IEnumerator DelayedPlayLevel()
        {
            yield return new WaitForSeconds(_playSound.length);
            GameController.GoToFirstLevel();

        }

        public void OnChangeButton()
        {
            if (_firstChange)
                _audioSource.PlayOneShot(_selectSound);
            else
                _firstChange = true;
        }

        private void StopEvents()
        {
            _eventSystem.enabled = false;
        }
    }
}
