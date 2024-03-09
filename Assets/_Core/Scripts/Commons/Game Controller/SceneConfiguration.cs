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
        public string GetLevel(int levelIndex)
        {
            if (levelIndex < gameplayLevels.Length)
                return gameplayLevels[levelIndex].Name;

            return endingScene.Name;
        }
    }
}