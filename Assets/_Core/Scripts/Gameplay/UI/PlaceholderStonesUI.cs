using Gameplay.Controller;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class PlaceholderStonesUI : MonoBehaviour
    {
        [SerializeField] private ResourcesController _resources;
        [SerializeField] private TextMeshProUGUI _textbox;

        private string _baseText;

        private void Awake()
        {
            _baseText = _textbox.text;
        }

        private void OnEnable()
        {
            _textbox.text = string.Format(_baseText, _resources.Stones, _resources.TotalStones);
            _resources.StonesChangeEvent += OnStonesChanged;
        }

        private void Start()
        {
            _textbox.text = string.Format(_baseText, _resources.Stones, _resources.TotalStones);
        }

        private void OnStonesChanged()
        {
            _textbox.text = string.Format(_baseText, _resources.Stones, _resources.TotalStones);
        }

        private void OnDisable()
        {
            _resources.StonesChangeEvent -= OnStonesChanged;
        }
    }
}