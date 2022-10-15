using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Mechanics
{
    public class Breakable : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _delayTime;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _visual;
        [SerializeField] private GameObject _animation;


        public void DoDamage()
        {
            _collider.enabled = false;
            _visual.SetActive(false);
            _animation.SetActive(true);
            StartCoroutine(WaitToRemove());
        }

        public IEnumerator WaitToRemove()
        {
            yield return new WaitForSeconds(_delayTime);
            Destroy(gameObject);
        }
    }
}
