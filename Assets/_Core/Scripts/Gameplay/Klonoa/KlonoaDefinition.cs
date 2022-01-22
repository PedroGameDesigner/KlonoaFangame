using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Gameplay.Klonoa
{
    [CreateAssetMenu(fileName = "Klonoa Definition", menuName = "Klonoa/Klonoa Definition")]
    public class KlonoaDefinition : ScriptableObject
    {
        [SerializeField] private SpeedData _moveSpeed;

        [SerializeField] private SpeedData _floatMoveSpeed;
        [SerializeField] private float _floatStartSpeed;
        [SerializeField] private float _floatHeight;
        [SerializeField] private float _floatTime;

        [SerializeField] private float _jumpSpeed = 5f;
        [SerializeField] private float _gravity = 15f;

        public SpeedData MoveSpeed => _moveSpeed;
        public SpeedData FloatMoveSpeed => _floatMoveSpeed;
        public float FloatStartSpeed => _floatStartSpeed;

        public float FloatAcceleration => 2 * (_floatHeight - _floatStartSpeed) / _floatTime;

        public float FloatTime
        {
            get { return _floatTime; }
        }

        public float JumpSpeed
        {
            get { return _jumpSpeed; }
        }

        public float Gravity
        {
            get { return _gravity; }
        }
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