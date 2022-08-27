using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gameplay.Klonoa;
using Gameplay.Controller;

namespace Gameplay.UI
{
    public class PlaceholderHealthUI : MonoBehaviour
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
            _textbox.text = string.Format(_baseText, _resources.Health);
            _resources.HealthChangeEvent += OnHealthChange;
        }

        private void Start()
        {
            _textbox.text = string.Format(_baseText, _resources.Health);
        }

        private void OnHealthChange()
        {
            _textbox.text = string.Format(_baseText, _resources.Health);
        }

        private void OnDisable()
        {
            _resources.HealthChangeEvent -= OnHealthChange;
        }
    }
}