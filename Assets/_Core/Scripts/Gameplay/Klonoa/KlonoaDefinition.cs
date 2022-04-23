using System;
using UnityEngine;
using Gameplay.Projectile;
using Sirenix.OdinInspector;

namespace Gameplay.Klonoa
{
    [CreateAssetMenu(fileName = "Klonoa Definition", menuName = "Klonoa/Klonoa Definition")]
    public class KlonoaDefinition : ScriptableObject
    {
        //Constants
        private const string GENERAL_GROUP = "General";
        private const string FLOAT_GROUP = "Float State";
        private const string JUMP_GROUP = "Jump State";
        private const string CAPTURE_GROUP = "Capture";
        private const string DAMAGE_GROUP = "Capture";

        //Attributes
        [FoldoutGroup(GENERAL_GROUP), SerializeField] private SpeedData _moveSpeed;
        [FoldoutGroup(GENERAL_GROUP), SerializeField] private int _maxHealth = 6;

        [FoldoutGroup(JUMP_GROUP), SerializeField] private float _jumpSpeed = 5f;
        [FoldoutGroup(JUMP_GROUP), SerializeField] private float _doubleJumpSpeed = 10f;
        [FoldoutGroup(JUMP_GROUP), SerializeField] private float _doubleJumpPreparationTime = 0.3f;
        [FoldoutGroup(JUMP_GROUP), SerializeField] private float _gravity = 15f;

        [FoldoutGroup(FLOAT_GROUP), SerializeField] private SpeedData _floatMoveSpeed;
        [FoldoutGroup(FLOAT_GROUP), SerializeField] private float _floatStartSpeed;
        [FoldoutGroup(FLOAT_GROUP), SerializeField] private float _floatHeight;
        [FoldoutGroup(FLOAT_GROUP), SerializeField] private float _floatTime;

        [FoldoutGroup(CAPTURE_GROUP), SerializeField] private CaptureProjectile _captureProjectile;
        [FoldoutGroup(CAPTURE_GROUP), SerializeField] private float _captureRepositionTime = 0.2f;

        [FoldoutGroup(DAMAGE_GROUP), SerializeField] private float _stunnedTime = 0.5f;
        [FoldoutGroup(DAMAGE_GROUP), SerializeField] private float _knockbackForce = 2f;
        [FoldoutGroup(DAMAGE_GROUP), SerializeField] private Vector3 _knockbackDirection = new Vector3(0, 0.7f, 0.7f);
        [FoldoutGroup(DAMAGE_GROUP), SerializeField] private float _invincibilityTime = 5f;

        //Accessors
        public SpeedData NotMoveSpeed => new SpeedData();
        public SpeedData MoveSpeed => _moveSpeed;
        public int MaxHealth => _maxHealth;

        public float JumpSpeed => _jumpSpeed;
        public float DoubleJumpSpeed => _doubleJumpSpeed;
        public float DoubleJumpPreparationTime => _doubleJumpPreparationTime;
        public float Gravity => _gravity;

        public SpeedData FloatMoveSpeed => _floatMoveSpeed;
        public float FloatStartSpeed => _floatStartSpeed;
        public float FloatAcceleration => 2 * (_floatHeight - _floatStartSpeed) / _floatTime;
        public float FloatTime => _floatTime;
        
        public CaptureProjectile CaptureProjectile => _captureProjectile;
        public float CaptureRepositionTime => _captureRepositionTime;

        public float StunnedTime => _stunnedTime;
        public float KnockbackForce => _knockbackForce;
        public Vector3 KnockbackDirection => _knockbackDirection.normalized;
        public float InvincibilityTime => _invincibilityTime;
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