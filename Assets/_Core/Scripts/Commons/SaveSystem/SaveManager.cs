using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        private const string SAVE_PATH = "/save.json";
        [SerializeField]
        private SaveData data;

        private string FullSavePath => Application.persistentDataPath + SAVE_PATH;

        [Button("Save")]
        public void Save()
        {
            Debug.Log("Save at " + FullSavePath);
            File.WriteAllText(FullSavePath, data.ToJson());
        }

        [Button("Load")]
        public void Load()
        {
            if (File.Exists(FullSavePath))
            {
                var json = File.ReadAllText(FullSavePath);
                data = JsonUtility.FromJson<SaveData>(json);
            }else
            {
                data = new SaveData();
                Save();
            }
        }

        public SaveData GetData()
        {
            return data;
        }

        public void UpdateData(SaveData data)
        {
            this.data = data;
        }
    }
}