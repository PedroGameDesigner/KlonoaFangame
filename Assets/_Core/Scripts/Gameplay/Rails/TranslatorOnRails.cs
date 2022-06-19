using PlatformerRails;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Rails
{
    public class TranslatorOnRails : MonoBehaviour, IMovile
    {
        [SerializeField] private RailBehaviour railBehaviour;
        [SerializeField] private Vector3 velocity;
        // Start is called before the first frame update
        public Vector3 Position { get; set; }
        public Vector3 Velocity
        {
            get => velocity;
            set => velocity = value;
        }

        IRail rail;

        void Awake()
        {
            if (railBehaviour == null)
                rail = RailManager.instance;
            else
                rail = railBehaviour;
        }

        void OnEnable()
        {
            StartCoroutine(RunLateFixedUpdate());
            UpdateLocalPosition();
        }

        void FixedUpdate()
        {
            Position += Velocity * Time.fixedDeltaTime;
            transform.position = rail.Local2World(Position);
        }

        void LateFixedUpdate()
        {
            UpdateLocalPosition();
        }

        void UpdateLocalPosition()
        {
            var w2l = rail.World2Local(transform.position);
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
        }

        IEnumerator RunLateFixedUpdate()
        {
            var wait = new WaitForFixedUpdate();
            while (true)
            {
                yield return wait;
                if (enabled) LateFixedUpdate();
            }
        }
    }
}
