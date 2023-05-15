using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using PlatformerRails;
using System;

namespace Colliders
{
    public class RigidbodyOnRails : MonoBehaviour, IMover
    {
        [SerializeField, FoldoutGroup("Configuration")]
        private RailBehaviour _railBehaviour;

        [SerializeField, FoldoutGroup("Colors")] 
        private Color _colliderColor = Color.white;
        [SerializeField, FoldoutGroup("Colors")]
        private bool _alwaysDrawGizmos = true;

        private Collider _collider;
        private Rigidbody _rigidbody;
        IRail _currentSingleRail;

        public event Action<IRail> RailChangeEvent;

        public Vector3 Position 
        { 
            get => Rail.World2Local(transform.position).Value; 
            set => transform.position = Rail.Local2World(value); 
        }
        public Vector3 Velocity { get; set; }

        public IRail Rail { get; private set; }

        private void Reset()
        {
            _collider = GetComponent<Collider>();

            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _rigidbody.freezeRotation = true;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            InitializeRail();
            StartCoroutine(RunLateFixedUpdate());
            UpdateLocalPosition();
        }

        private void InitializeRail()
        {
            if (_railBehaviour == null)
                Rail = RailManager.instance;
            else
                Rail = _railBehaviour;
        }

        private void OnDisable()
        {
            StopCoroutine(RunLateFixedUpdate());
            Velocity = Vector3.zero;
        }

        private void FixedUpdate()
        {
            Vector3 expectedPosition =
                Rail.Local2World(Position + Velocity * Time.fixedDeltaTime);
            Vector3 direction = (expectedPosition - transform.position).normalized;

            _rigidbody.velocity = direction * Velocity.magnitude;
        }

        private void LateFixedUpdate()
        {
            UpdateLocalPosition();
        }
        private void UpdateLocalPosition()
        {
            IRail usedRail;
            var w2l = Rail.World2Local(transform.position, out usedRail);
            if (w2l == null)
            {
                Destroy(gameObject);
                return;
            }

            var newrot = Rail.Rotation(Position.z);
            if (Quaternion.Angle(transform.rotation, newrot) > 30f)
                Velocity = Quaternion.Inverse(newrot) * transform.rotation * Velocity;
            transform.rotation = newrot;
            CheckUsedRail(usedRail);
        }

        private IEnumerator RunLateFixedUpdate()
        {
            var wait = new WaitForFixedUpdate();
            while (true)
            {
                yield return wait;
                LateFixedUpdate();
            }
        }

        private void CheckUsedRail(IRail usedRail)
        {
            if (usedRail != _currentSingleRail)
            {
                RailChangeEvent?.Invoke(usedRail);
                _currentSingleRail = usedRail;
            }
        }

        private void OnDrawGizmos()
        {
            if (_alwaysDrawGizmos)
                DrawGizmos();
        }

        private void OnDrawGizmosSelected()
        {
            if (!_alwaysDrawGizmos)
                DrawGizmos();
        }

        private void DrawGizmos()
        {

#if UNITY_EDITOR
            if (_collider == null)
                _collider = GetComponent<Collider>();
#endif
            if (_collider == null) return;
            Gizmos.color = _colliderColor;
            _collider.DrawAsGizmo();
        }
    }
}
