using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gameplay.Klonoa;

namespace Gameplay.UI
{
    public class PlaceholderHealthUI : MonoBehaviour
    {
        [SerializeField] private KlonoaBehaviour _klonoa;
        [SerializeField] private TextMeshProUGUI _textbox;

        private string _baseText;

        private void Awake()
        {
            _baseText = _textbox.text;
        }

        private void OnEnable()
        {
            _textbox.text = string.Format(_baseText, _klonoa.Health);
            _klonoa.DamageEvent += OnKlonoaDamage;
        }

        private void Start()
        {
            _textbox.text = string.Format(_baseText, _klonoa.Health);
        }

        private void OnKlonoaDamage()
        {
            _textbox.text = string.Format(_baseText, _klonoa.Health);
        }

        private void OnDisable()
        {
            _klonoa.DamageEvent -= OnKlonoaDamage;
        }
    }
}