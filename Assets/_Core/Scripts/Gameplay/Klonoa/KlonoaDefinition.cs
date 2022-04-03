using System;
using UnityEngine;
using Gameplay.Projectile;

namespace Gameplay.Klonoa
{
    [CreateAssetMenu(fileName = "Klonoa Definition", menuName = "Klonoa/Klonoa Definition")]
    public class KlonoaDefinition : ScriptableObject
    {
        [SerializeField] private SpeedData _moveSpeed;
        [Space()]
        [SerializeField] private SpeedData _floatMoveSpeed;
        [SerializeField] private float _floatStartSpeed;
        [SerializeField] private float _floatHeight;
        [SerializeField] private float _floatTime;
        [Space()]
        [SerializeField] private float _jumpSpeed = 5f;
        [SerializeField] private float _gravity = 15f;
        [Space()]
        [SerializeField] private float _doubleJumpSpeed = 10f;
        [SerializeField] private float _doubleJumppreparationTime = 0.3f;
        [Space()]
        [SerializeField] private CaptureProjectile _captureProjectile;
        [SerializeField] private float _captureRepositionTime = 0.2f;

        public SpeedData NotMoveSpeed => new SpeedData();
        public SpeedData MoveSpeed => _moveSpeed;
        public SpeedData FloatMoveSpeed => _floatMoveSpeed;
        public float FloatStartSpeed => _floatStartSpeed;
        public float FloatAcceleration => 2 * (_floatHeight - _floatStartSpeed) / _floatTime;
        public float FloatTime => _floatTime;
        public float JumpSpeed => _jumpSpeed;
        public float DoubleJumpSpeed => _doubleJumpSpeed;
        public float DoubleJumpPreparationTime => _doubleJumppreparationTime;
        public float Gravity => _gravity;
        public CaptureProjectile CaptureProjectile => _captureProjectile;
        public float CaptureRepositionTime => _captureRepositionTime;
    }

    [Serializable]
    public class SpeedData
    {
        [SerializeField] private float _accelaration = 20f;
        [SerializeField] private float _drag = 5f;

        public float Acceleration
        {
            get { return _accelaration; }
        }

        public float Drag
        {
            get { return _drag; }
        }
    }

}