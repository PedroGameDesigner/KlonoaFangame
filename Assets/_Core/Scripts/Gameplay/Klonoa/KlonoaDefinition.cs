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
        [SerializeField] private float _jumpSpeed = 5f;
        [SerializeField] private float _gravity = 15f;

        public SpeedData MoveSpeed
        {
            get { return _moveSpeed; }
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