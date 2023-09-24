using PlatformerRails;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace EditorTools
{
    public class InstantiateOnRailMenu : OdinEditorWindow
    {
        [SerializeField] private Transform _prefab;
        [SerializeField] private RailBehaviour _rail;
        [SerializeField] private Vector2 _displacement;
        [SerializeField] private bool _relativeDistance;
        [SerializeField] private bool _childOfSelected;
        [SerializeField, EnableIf("RailReady"), PropertyRange(0, "RailLength")] private float _distance;

        [SerializeField, TabGroup("Multiple Objects"), Min(1)]
        private int _copyCount = 1;
        [SerializeField, TabGroup("Multiple Objects")]
        private Vector3 _separation = Vector3.forward * 0.5f;

        private Transform _cursor;

        private bool RailReady => _rail != null;
        private bool PrefabReady => _prefab != null;
        private bool AllReady => RailReady && PrefabReady;

        private float Distance => _relativeDistance ? _distance * _rail.Length : _distance;
        private float RailLength => _relativeDistance || !RailReady ? 1 : _rail.Length;

        private Transform InstanceParent => _childOfSelected ? GetFirstSelected() : null;

        [MenuItem("Tools/Instantiate On Rail")]
        private static void OpenWindow()
        {
            GetWindow<InstantiateOnRailMenu>().Show();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            InstantiateCursor();
        }

        private void InstantiateCursor()
        {
            if (_cursor == null)
            {
                GameObject cursor = GameObject.Find("Rail Instance Cursor");
                if (cursor == null)
                    _cursor = new GameObject("Rail Instance Cursor").transform;
                else
                    _cursor = cursor.transform;

                if (_cursor.GetComponent<SimpleGizmo>() == null)
                    _cursor.gameObject.AddComponent<SimpleGizmo>();
            }
        }

        private void OnValidate()
        {
            if (_cursor != null && RailReady)
            {
                _cursor.transform.position =
                    RailLocalPosition(Distance, _displacement.y, _displacement.x);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            DestroyCursor();
        }
        private void DestroyCursor()
        {
            if (_cursor != null)
            {
                DestroyImmediate(_cursor.gameObject);
            }
        }


        [Button("Instantiate"), EnableIf("AllReady"), TabGroup("Single Object")]
        public void InstantiateSingle()
        {
            Vector3 position = RailLocalPosition(Distance, _displacement.y, _displacement.x);
            Transform parent = InstanceParent;
            Transform instance = IntantiatePrefab(_prefab, position, Quaternion.identity, parent);
        }

        [Button("Instantiate"), EnableIf("AllReady"), TabGroup("Multiple Objects")]
        public void InstantiateMultiple()
        {
            Vector3 basePosition = RailLocalPosition(Distance, _displacement.y, _displacement.x);
            Transform parent = InstanceParent;
            for (int i = 0; i < _copyCount; i++)
            {
                Vector3 displacement = (Vector3)_displacement + _separation * i;
                Vector3 position = RailLocalPosition(Distance + displacement.z, displacement.y, displacement.x) ;
                Transform instance = IntantiatePrefab(_prefab, position, Quaternion.identity, parent);
            }
        }
        private Vector3 RailLocalPosition(float distance, float height, float displacement)
        {
            float clampDistance = Mathf.Clamp(distance, 0, _rail.Length);
            Vector3 localPosition = new Vector3(displacement, height, clampDistance);
            return _rail.Local2World(localPosition);
        }

        private Transform GetFirstSelected()
        {
            return Selection.GetTransforms(
                SelectionMode.TopLevel | SelectionMode.ExcludePrefab | SelectionMode.Editable)
                .FirstOrDefault();
        }

        private Transform IntantiatePrefab(Transform transform, Vector3 position, Quaternion rotation, Transform parent)
        {
            var prefab = PrefabUtility.GetCorrespondingObjectFromSource(transform);
            if (prefab != null)
            {
                var instance = (Transform)PrefabUtility.InstantiatePrefab(prefab, parent);
                instance.position = position;
                instance.rotation = rotation;
                return instance;
            }
            else
                return Instantiate(transform, position, Quaternion.identity, parent);
        }
    }
}