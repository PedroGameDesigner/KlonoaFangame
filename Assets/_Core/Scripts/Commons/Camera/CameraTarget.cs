using Gameplay.Klonoa;
using PlatformerRails;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cameras
{
    [ExecuteAlways]
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private KlonoaBehaviour _target;
        [SerializeField] private CameraConfiguration _config;
        [Space]
        [SerializeField] private RailBehaviour _debugRail;

        private float _lastY;

        public Vector3 PathNormal => Vector3.Cross(Vector3.up, _target.transform.forward).normalized;

        private bool TargetIsGrounded => !Application.isPlaying || _target.IsGrounded;
        private bool ShouldUpdate => Application.isPlaying || _debugRail != null;
        private IRail Rail => !Application.isPlaying
            ? _debugRail
            : _target.Mover.Rail;

        private Vector3 TargetPosition => Application.isPlaying ?
            _target.Mover.Position :
            Rail.World2Local(_target.transform.position).Value;


        private void LateUpdate()
        {
            if (!ShouldUpdate) return;
            Vector3 nextPosition = UpdateNextPosition();
            nextPosition.y = UpdateYPosition(nextPosition.y);
            transform.position = _target.Mover.Rail.Local2World(nextPosition);

            _lastY = nextPosition.y;
        }

        private Vector3 UpdateNextPosition()
        {
            return TargetPosition +
                _target.HorizontalFacing.GetVector() * _config.Displacement.z +
                Vector3.up * _config.Displacement.y;             
        }

        private float UpdateYPosition(float nextY)
        {
            return TargetIsGrounded ? nextY : _lastY;
        }
    }
}