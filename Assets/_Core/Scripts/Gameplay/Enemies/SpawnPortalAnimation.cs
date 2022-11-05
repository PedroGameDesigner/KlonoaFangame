using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class SpawnPortalAnimation : MonoBehaviour
    {
        public const string VISIBLE_PARAMETER = "Visible"; 

        [SerializeField] private SpawnPortal _portal;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _portal.StartSpawnEvent += OnSpawnStart;
            _portal.EndSpawnEvent += OnSpawnEnd;
        }

        private void OnSpawnStart()
        {
            _animator.SetBool(VISIBLE_PARAMETER, true);
        }

        private void OnSpawnEnd()
        {
            _animator.SetBool(VISIBLE_PARAMETER, false);
        }
    }
}
