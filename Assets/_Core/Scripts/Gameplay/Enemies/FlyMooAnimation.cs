using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class FlyMooAnimation : MonoBehaviour
    {
        [SerializeField] private FlyMooBehaviour _behaviour;
        [SerializeField] private SpriteRenderer _renderer;

        private void Update()
        {
            if (_behaviour.Facing >= 0)
                _renderer.flipX = false;
            else
                _renderer.flipX = true;
        }
    }
}