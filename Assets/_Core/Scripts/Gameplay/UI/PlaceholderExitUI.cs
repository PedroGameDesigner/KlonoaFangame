
using Coffee.UIExtensions;
using Gameplay.Controller;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class PlaceholderExitUI : MonoBehaviour
    {
        [SerializeField] private ExitController _controller;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private UIShadow _shadow;
        [Space]
        [SerializeField] private float _timeProportion;
        [SerializeField] private Gradient _bodyColor;
        [SerializeField] private Gradient _outlineColor;

        private bool _inProcess;

        private float Time => _controller.ExitingPercent / _timeProportion;

        public void Update()
        {
            if (Time >= 0)
            {
                UpdateColors();
                _inProcess = true;
            }
            else if (_inProcess)
            {
                ResetColors();
                _inProcess = false;
            }
        }

        private void UpdateColors()
        {
            _text.color = _bodyColor.Evaluate(Time);
            _shadow.effectColor = _outlineColor.Evaluate(Time);
        }

        private void ResetColors()
        {
            _text.color = _bodyColor.Evaluate(0);
            _shadow.effectColor = _outlineColor.Evaluate(0);
        }
    }
}
