using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [System.Serializable]
    public class SaveData
    {
        public int lastLevel = 0;
        public bool gameCompleted = false;
        public List<SaveLevelData> levelsData;

        public string ToJson() => JsonUtility.ToJson(this);

        public SaveLevelData GetLevelData(int index)
        {
            while (index >= levelsData.Count) 
            {
                levelsData.Add(new SaveLevelData());
            }

            return levelsData[index];
        }
    }

    [System.Serializable]
    public class SaveLevelData
    {
        public bool levelCompleted = false;
        public int dreamStones = 0;
        public bool moonShard = false;
        public bool darkMoonShard = false;

        public string ToJson() => JsonUtility.ToJson(this);
    }
}