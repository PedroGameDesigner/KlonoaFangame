using Extensions;
using GameControl;
using Gameplay.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class EndLevelTrigger : MonoBehaviour
    {
        [SerializeField] private bool _useLayer;
        [SerializeField] LayerMask _detectionLayer;
        [SerializeField] private bool _useTag;
        [SerializeField] private string _detectionTag;

        private GameplayController _controller;
        private int _objectsInside;

        private void Awake()
        {
            _controller = FindFirstObjectByType<GameplayController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (EvaluateTarget(other.gameObject))
            {
                Debug.Log($"OnTriggerEnter({other.name})");
                _objectsInside++;
                _controller.EndLevel();
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