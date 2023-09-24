using Gameplay.Klonoa;
using PlatformerRails;
using System.Collections;
using UnityEngine;

namespace Cameras
{
    [ExecuteAlways]
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private KlonoaBehaviour _target;
        [Space]
        [SerializeField] private Quaternion _baseRotation;
        [SerializeField] private float _cameraDistanceProportion = 1;
        [SerializeField] private AnimationCurve _facingRepositionCurve;

        [Space]
        [SerializeField] private RailBehaviour _debugRail;

        private float _lastY;
        private FaceDirection _lastFaceDirection;
        private float _currentForwardDisplacement;
        private float _distanceToCamera;

        private Coroutine _moveFacingPosition;
        private Coroutine _moveRelativePosition;

        public bool FollowOnAir { get; set; }
        public bool FollowOnGround { get; set; }
        public Vector3 PathNormal => Vector3.Cross(Vector3.up, _target.transform.forward).normalized;

        private bool MustUpdateYPosition =>
            TargetIsGrounded && FollowOnGround ||
            !TargetIsGrounded && FollowOnAir;
        private float ForwardDisplacementDistance => _distanceToCamera * _cameraDistanceProportion;
        private bool TargetIsGrounded => !Application.isPlaying || _target.IsGrounded;
        private bool ShouldUpdate => Application.isPlaying || _debugRail != null;
        private IRail Rail => !Application.isPlaying
            ? _debugRail
            : _target.Mover.Rail;

        private Vector3 TargetPosition => Application.isPlaying ?
            _target.Mover.Position :
            Rail.World2Local(_target.transform.position).Value;
        private float FacingPositionTime => _facingRepositionCurve.keys.Length <= 0 ? 1 :
            _facingRepositionCurve.keys[_facingRepositionCurve.keys.Length - 1].time;

        private void Start()
        {
            _lastFaceDirection = _target.HorizontalFacing;
            _currentForwardDisplacement = _lastFaceDirection.GetVector().z;
        }

        private void LateUpdate()
        {
            if (!ShouldUpdate) return;
            UpdateFacing();
            Vector3 nextPosition = UpdateNextPosition();
            nextPosition.y = UpdateYPosition(nextPosition.y);
            transform.position = Rail.Local2World(nextPosition);
            transform.rotation = _baseRotation * _target.transform.rotation;

            _lastY = nextPosition.y;
        }

        private void UpdateFacing()
        {
            if (_lastFaceDirection != _target.HorizontalFacing)
            {
                if (_moveFacingPosition != null)
                    StopCoroutine(_moveFacingPosition);
                
                _lastFaceDirection = _target.HorizontalFacing;
                _moveFacingPosition = StartCoroutine(MoveFacinPosition(_target.HorizontalFacing.GetVector()));
            }
        }

        private IEnumerator MoveFacinPosition(Vector3 facingDirection)
        {
            float startDisplacement = _currentForwardDisplacement;
            float timer = 0;
            
            while(timer < FacingPositionTime)
            {                
                float proportion = _facingRepositionCurve.Evaluate(timer / FacingPositionTime);
                _currentForwardDisplacement = Mathf.Lerp(startDisplacement, facingDirection.z, proportion);
                yield return null;
                timer += Time.deltaTime;
            }
            _currentForwardDisplacement = facingDirection.z;
            _moveRelativePosition = null;
        }

        private Vector3 UpdateNextPosition()
        {
            return TargetPosition + ForwardDisplacementDistance * _currentForwardDisplacement * Vector3.forward;
        }

        private float UpdateYPosition(float nextY)
        {
            return MustUpdateYPosition ? nextY : _lastY;
        }

        public void UpdateRelativePosition(float distanceToCamera, float time)
        {
            if (_moveRelativePosition != null)
                StopCoroutine(_moveRelativePosition);

            _moveRelativePosition = StartCoroutine(MoveRelativePosition(distanceToCamera, time));
        }

        private IEnumerator MoveRelativePosition(float distanceToCamera, float time)
        {
            float oldDistance = _distanceToCamera;
            float timer = 0;

            while (timer < time)
            {
                _distanceToCamera = Mathf.Lerp(oldDistance, distanceToCamera, timer / time);
                yield return null;
                timer += Time.deltaTime;
            }
        }
    }
}