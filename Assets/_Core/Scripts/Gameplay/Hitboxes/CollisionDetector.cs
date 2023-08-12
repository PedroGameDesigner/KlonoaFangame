using Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Hitboxes
{
    public class CollisionDetector : MonoBehaviour
    {
        private const string LAYER_NAME = "Collider";

        [SerializeField] private LayerMask _collisionLayer;
        [SerializeField] private UnityEvent<Collision> _collisionEvent;

        private void OnCollisionEnter(Collision collision)
        {
            if (enabled && _collisionLayer.CheckLayer(collision.gameObject.layer))            
                _collisionEvent.Invoke(collision);
        }

        private void OnValidate()
        {
            var collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.isTrigger = false;
            }

            gameObject.layer = LayerMask.NameToLayer(LAYER_NAME);
        }
    }
}