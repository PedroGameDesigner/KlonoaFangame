using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Mechanics
{
    public class TornadoAnimations : MonoBehaviour
    {
        private const string BOUNCE_TRIGGER = "Bounce";

        [SerializeField] private Tornado _tornado;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform[] _rings;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private Vector3 _translation;
        [SerializeField] private float _translationMultiplier;
        [SerializeField] private float _animationDelay;

        private Vector3[] _ringsPosition;
        private float _timer;

        private float CurveTime => _curve.keys[_curve.keys.Length - 1].time;

        private void Awake()
        {
            _tornado.BounceEvent += OnBounce;
            _ringsPosition = new Vector3[_rings.Length];
            for (int i = 0; i < _rings.Length; i++)
            {
                _ringsPosition[i] = _rings[i].localPosition;
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            DisplazeRings();
        }

        private void DisplazeRings()
        {
            for (int i = 0; i < _rings.Length; i++)
            {
                Vector3 displacement = GetDisplacement(i);
                _rings[i].localPosition = _ringsPosition[i] + displacement;
            }
        }

        private Vector3 GetDisplacement(int index)
        {
            float clampTimer = (_timer + _animationDelay * index) % CurveTime;
            Vector3 realTranslation = CalculateTranslation(index);
            return _curve.Evaluate(clampTimer) * realTranslation;
        }

        private Vector3 CalculateTranslation(int index)
        {
            return _translation.normalized * (_translation.magnitude * (1 - _translationMultiplier * index));
        }

        private void OnBounce()
        {
            _animator.SetTrigger(BOUNCE_TRIGGER);
        }
    }
}
