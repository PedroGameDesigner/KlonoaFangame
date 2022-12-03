using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class FlyMooBehaviour : EnemyBehaviour
    {
        public float debugPercent;
        public float debugLocalPercent;
        [SerializeField] private SplineFollower _splineFollower;
        [SerializeField] private bool _loop;
        [SerializeField] private AnimationCurve _accelerationCurve;
        [SerializeField] private float _defaultFacing = 1;
        [SerializeField] private double _startAccelerationArea;
        [SerializeField] private double _endAccelerationArea;

        public override bool CanBeCaptured => true;
        private float MinSpeed => _definition.MoveSpeed * 0.35f;
        public float Facing 
        {
            get
            {
                if (_splineFollower == null)
                    return _defaultFacing;
                else
                    return _splineFollower.direction == Spline.Direction.Forward ? 1 : -1;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        private void Initialize()
        {
            if (_splineFollower != null)
            {
                _splineFollower.followSpeed = _definition.MoveSpeed;
                _splineFollower.SetPercent(0);

                if (_loop) _splineFollower.wrapMode = SplineFollower.Wrap.Loop;
                else _splineFollower.wrapMode = SplineFollower.Wrap.PingPong;
            }
        }

        protected void FixedUpdate()
        {
            if (!_loop && _splineFollower != null) 
                UpdatePingPongSpeed();
        }

        protected void UpdatePingPongSpeed()
        {
            double pathPercent = _splineFollower.GetPercent();
            debugPercent = (float) pathPercent;
            if (pathPercent <= _startAccelerationArea)
                _splineFollower.followSpeed = StartAccelerationSpeed(pathPercent);
            else if (pathPercent >= _endAccelerationArea)
                _splineFollower.followSpeed = EndAccelerationSpeed(pathPercent);
            else
                _splineFollower.followSpeed = _definition.MoveSpeed;
        }

        protected float StartAccelerationSpeed(double percent)
        {
            float value = (float)(percent / _startAccelerationArea);
            debugLocalPercent = value;
            return Mathf.Max(MinSpeed, _definition.MoveSpeed * _accelerationCurve.Evaluate(value));
        }

        protected float EndAccelerationSpeed(double percent)
        {
            float value = (float)((1 - percent) / (1 - _endAccelerationArea));
            debugLocalPercent = value;
            return Mathf.Max(MinSpeed, _definition.MoveSpeed * _accelerationCurve.Evaluate(value));
        }

        protected override void ChangeToActiveState()
        {
            base.ChangeToActiveState();
            if (_splineFollower != null) _splineFollower.enabled = true;
            Initialize();
        }

        protected override void Kill()
        {
            if (_splineFollower != null) _splineFollower.enabled = false;

            base.Kill();
        }
    }
}
