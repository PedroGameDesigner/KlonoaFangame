using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class KlonoaParticles : MonoBehaviour
    {
        [SerializeField] private KlonoaBehaviour _behaviour;
        [Space]
        [SerializeField] private ParticleSystem _captureParticles;

        // Start is called before the first frame update
        private void Awake()
        {
            _behaviour.CaptureProjectileEvent += OnCaptureProjectile;
        }

        private void OnCaptureProjectile()
        {
            _captureParticles.Play(true);
        }
    }
}
