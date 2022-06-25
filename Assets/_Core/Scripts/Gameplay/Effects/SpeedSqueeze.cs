using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Gameplay.Effects 
{
    public class SpeedSqueeze : MonoBehaviour
    {
        [ValidateInput("MustBeIMovile", "TMust be IMovile.", InfoMessageType.Error)]
        [SerializeField] private MonoBehaviour _velocityReference;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private Vector2 _scaleRange;

        private IMovile VelocityReference => _velocityReference as IMovile;
        private float MinScale => _scaleRange[0];
        private float MaxScale => _scaleRange[1];

        private void Update()
        {
            Vector3 normalVelocity = GetNormalVelocity();
            transform.localScale = CalculateScale(normalVelocity);
        }

        private Vector3 GetNormalVelocity()
        {
            Vector3 velocity = VelocityReference.Velocity;
            return new Vector3(
                Mathf.Clamp(velocity.x, -_maxVelocity, _maxVelocity) / _maxVelocity,
                Mathf.Clamp(velocity.y, -_maxVelocity, _maxVelocity) / _maxVelocity,
                Mathf.Clamp(velocity.z, -_maxVelocity, _maxVelocity) / _maxVelocity
                );
        }

        private Vector3 CalculateScale(Vector3 velocity)
        {
            float scaleX = 1 + (Mathf.Abs(velocity.x) * (MaxScale - 1));
            float scaleY = 1 + (Mathf.Abs(velocity.y) * (MaxScale - 1));
            float actualScaleX = Mathf.Max(MinScale, scaleX - (scaleY - 1));
            float actualScaleY = Mathf.Max(MinScale, scaleY - (scaleX - 1));
            return new Vector3(actualScaleX, actualScaleY, 1);
        }

        private bool MustBeIMovile(MonoBehaviour script)
        {
            return typeof(IMovile).IsAssignableFrom(script.GetType());
        }

        private void OnDrawGizmos()
        {
            
        }
    }
}
