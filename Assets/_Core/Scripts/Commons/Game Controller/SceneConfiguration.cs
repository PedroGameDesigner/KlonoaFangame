using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameControl
{
    [CreateAssetMenu(fileName = "SceneConfiguration", menuName = "Game Control/SceneConfiguration")]
    public class SceneConfiguration : ScriptableObject
    {
        [SerializeField]
        private SceneObject mainMenu;
        public string MainMenu => mainMenu.Name;

        [SerializeField]
        private SceneObject[] gameplayLevels;
        [SerializeField]
        private SceneObject endingScene;
        public string FirstLevel => gameplayLevels[0].Name;
        public string EndingScene => endingScene.Name;

        public bool IsLastLevel(int levelIndex)
        {
            return levelIndex >= gameplayLevels.Length;
        }

        public string GetLevel(int levelIndex)
        {
            return endingScene.Name;
        }
    }
}