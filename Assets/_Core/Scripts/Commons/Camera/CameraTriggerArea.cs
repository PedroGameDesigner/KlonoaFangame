using Cinemachine;
using Extensions;
using UnityEngine;

namespace Cameras
{
    public class CameraTriggerArea : MonoBehaviour
    {
        private const int SELECTED_PRIORITY = 100;
        private const int UNSELECTED_PRIORITY = 0;
        private const int DEFAULT_PRIORITY = 10;

        [SerializeField] private bool _defaultCamera;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [Space]
        [SerializeField] private bool _useLayer;
        [SerializeField] LayerMask _detectionLayer;
        [SerializeField] private bool _useTag;
        [SerializeField] private string _detectionTag;

        private int _objectsInside;

        private void Start()
        {
            _camera.enabled = _defaultCamera;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (EvaluateTarget(other.gameObject))
            {
                Debug.Log($"OnTriggerEnter({other.name})");
                _objectsInside++;
                _camera.gameObject.SetActive(_objectsInside > 0);
                _camera.enabled = _objectsInside > 0;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (EvaluateTarget(other.gameObject))
            {
                Debug.Log($"OnTriggerExit({other.name})");
                _objectsInside--;
                _camera.enabled = _objectsInside > 0;
            }
        }

        private bool EvaluateTarget(GameObject gameObject)
        {
            bool result = true;

            if (_useLayer)
                result &= _detectionLayer.CheckLayer(gameObject.layer);
            if (_useTag)
                result &= gameObject.CompareTag(_detectionTag);

            return result;
        }
    }
}