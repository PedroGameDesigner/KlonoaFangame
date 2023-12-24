using PlatformerRails;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colliders
{
    public class CharacterOnRails : CylinderCharacterController.CharacterController, IMover
    {
        [SerializeField]
        RailBehaviour railBehaviour;

        IRail rail;
        IRail currentSingleRail;

        public Vector3 Position { get; set; }
        public override Vector3 Velocity { get; set; }
        public IRail Rail => rail;
        public IRail CurrentSingleRail => currentSingleRail;
        public bool IsGrounded => physics.IsOnFloor;
        public bool IsTouchingCeiling => physics.IsTouchingCeiling;


        public event System.Action OnLocalPositionUpdated;
        public event System.Action<IRail> RailChangeEvent;

        protected override void Awake()
        {
            base.Awake();
            if (railBehaviour == null)
                rail = RailManager.instance;
            else
                rail = railBehaviour;
        }

        void OnEnable()
        {
            StartCoroutine(RunLateFixedUpdate());
            //UpdateLocalPosition();
        }

        void OnDisable()
        {
            StopCoroutine(RunLateFixedUpdate());
            Velocity = Vector3.zero;
        }

        protected override void FixedUpdate()
        {
            Position += Velocity * Time.fixedDeltaTime;
            Vector3 worldPosition = rail.Local2World(Position);
            //transform.position = worldPosition;
        }

        void LateFixedUpdate()
        {
            UpdateLocalPosition();
        }

        public void UpdateLocalPosition()
        {
            IRail usedRail;
            var w2l = rail.World2Local(transform.position, out usedRail);
            if (w2l == null)
            {
                Destroy(gameObject);
                return;
            }
            Position = w2l.Value;

            var newrot = rail.Rotation(Position.z);
            if (Quaternion.Angle(transform.rotation, newrot) > 30f)
                Velocity = Quaternion.Inverse(newrot) * transform.rotation * Velocity;
            transform.rotation = newrot;
            physics.Speed = transform.rotation * Velocity;

            CheckUsedRail(usedRail);
            OnLocalPositionUpdated?.Invoke();
        }

        IEnumerator RunLateFixedUpdate()
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
            if (usedRail != currentSingleRail)
            {
                Debug.Log($"Character {name}: {currentSingleRail} => {usedRail}");
                //Debug.Break();
                RailChangeEvent?.Invoke(usedRail);
                currentSingleRail = usedRail;
            }
        }
    }
}