using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class KlonoaHitbox : MonoBehaviour
    {
        [SerializeField]
        private KlonoaBehaviour _klonoa;
        [Space]
        [SerializeField]
        private string _enemyTag;
        [SerializeField]
        private string _collectableTag;
        [SerializeField]
        private string _deathPlaneTag;

        private void OnCollisionEnter(Collision collision)
        {
          /*  Debug.Log("CollidionEnter: " + collision.collider.name);
            if (collision.collider.CompareTag(_enemyTag))
                _klonoa.OnDamage(collision);
            else if (collision.collider.CompareTag(_collectableTag))
                _klonoa.OnCollectableDetected(collision);
            else if (collision.collider.CompareTag(_deathPlaneTag))
                _klonoa.Death();*/
        }
    }
}
