using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    [CreateAssetMenu(fileName = "Klonoa Definition", menuName = "Klonoa/Klonoa Definition")]
    public class KlonoaDefinition : ScriptableObject
    {
        [SerializeField] private float _accelaration = 20f;
        [SerializeField] private float _drag = 5f;
        [SerializeField] private float _jumpSpeed = 5f;
        [SerializeField] private float _gravity = 15f;

        public float Acceleration
        {
            get { return _accelaration; }
        }

        public float Drag
        {
            get { return _drag; }
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
}