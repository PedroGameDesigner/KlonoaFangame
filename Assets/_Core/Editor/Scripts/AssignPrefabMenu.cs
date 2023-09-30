using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorTools
{
    public class AssignPrefabMenu : OdinEditorWindow
    {
        [SerializeField] private Transform _prefab;

        [MenuItem("Tools/Asign Prefab to Selected")]
        private static void OpenWindow()
        {
            GetWindow<AssignPrefabMenu>().Show();
        }


        [Button("Convert Selected")]
        public void ConvertSelected()
        {
            if (Selection.gameObjects.Length > 0)
            {
                for (int i = 0; i < Selection.gameObjects.Length; i++) 
                {
                    var refTransform = Selection.gameObjects[i].transform;
                    var newInstance = (Transform)PrefabUtility.InstantiatePrefab(_prefab, refTransform.parent);
                    newInstance.position = refTransform.position;
                    newInstance.rotation = refTransform.rotation;
                    newInstance.name = refTransform.name;
                    Selection.gameObjects[i] = newInstance.gameObject;
                    GameObject.DestroyImmediate(refTransform.gameObject);
                }
            }
        }
    }
}