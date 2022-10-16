using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    [CreateAssetMenu(fileName = "Enemy Definition", menuName = "Enemy/Enemy Definition")]
    public class EnemyDefinition : ScriptableObject
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _stopTime;

        public float MoveSpeed => _moveSpeed;
        public float StopTime => _stopTime;
    }
}