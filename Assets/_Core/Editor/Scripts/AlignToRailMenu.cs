using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using PlatformerRails;
using Sirenix.OdinInspector;

namespace EditorTools
{
    public class AlignToRailMenu : OdinEditorWindow
    {
        [MenuItem("Tools/Align to rail")]
        private static void OpenWindow()
        {
            GetWindow<AlignToRailMenu>().Show();
        }

        public PathBase _rail;

        [Button(ButtonSizes.Large)]
        public void AlingSelectedToRail() 
        { 
            foreach(Transform selected in Selection.transforms)
            {
                if (selected != _rail.transform)
                    AlignObjectToRail(selected);
            }
        }

        private void AlignObjectToRail(Transform transform)
        {
            Vector3? localPositionNullable = _rail.World2Local(transform.position);
            if (localPositionNullable != null)
            {
                Vector3 localPosition = localPositionNullable.Value;
                localPosition = new Vector3(0, localPosition.y, localPosition.z); ;
                transform.position = _rail.Local2World(localPosition);
            }
        }

        [Button(ButtonSizes.Large)]
        public void RotateSelectedToRail()
        {
            foreach (Transform selected in Selection.transforms)
            {
                if (selected != _rail.transform)
                    RotateObjectToRail(selected);
            }
        }

        private void RotateObjectToRail(Transform transform)
        {
            Vector3? localPositionNullable = _rail.World2Local(transform.position);
            if (localPositionNullable != null)
            {
                Vector3 localPosition = localPositionNullable.Value;
                localPosition = new Vector3(0, localPosition.y, localPosition.z);
                Vector3 advancement = localPosition + Vector3.forward;
                localPosition = _rail.Local2World(localPosition);
                advancement = _rail.Local2World(advancement);
                transform.rotation = Quaternion.LookRotation(advancement - localPosition, Vector3.up);
            }
        }
    }
}
