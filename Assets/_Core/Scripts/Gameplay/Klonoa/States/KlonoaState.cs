using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using PlatformerRails;

namespace Gameplay.Klonoa
{
    public abstract class KlonoaState : State<KlonoaBehaviour>
    {
        protected Vector2 _inputDirection;
        protected Vector2 _lastDirection;

        protected readonly KlonoaDefinition _definition;
        protected readonly MoverOnRails _mover;

        protected abstract SpeedData MoveSpeed { get; }
        protected abstract float Gravity { get; }
        protected abstract bool CanTurn { get; }

        protected KlonoaState(KlonoaBehaviour behaviour) : base(behaviour) 
        {
            _mover = behaviour.GetComponent<MoverOnRails>();
            _definition = behaviour.Definition;
        }

        public override void Enter()
        {
            SubscribeToKlonoaEvent();
        }

        public override void Update(float deltaTime) { }

        public override void FixedUpdate(float deltaTime)
        {            
            UpdateMove(deltaTime);
            UpdateGravity( deltaTime);
        }

        protected void UpdateMove(float deltaTime)
        {
            if (MoveSpeed.Acceleration <= 0) return;
            //Changing Z value in local position means moving toward rail direction
            Vector3 velocity = _mover.Velocity;
            if (CanTurn)
                _lastDirection = _inputDirection;
            velocity.z += _lastDirection.x * MoveSpeed.Acceleration * deltaTime;
            velocity.z -= _mover.Velocity.z * MoveSpeed.Drag * deltaTime;
            _mover.Velocity = velocity;
        }

        protected void UpdateGravity(float deltaTime)
        {
            if (Gravity <= 0) return;
            //Y+ axis = Upwoard (depends on rail rotation)
            Vector3 velocity = _mover.Velocity;
            if (_behaviour.CollisionData.Grounded)
            {
                velocity.y = (_behaviour.CollisionData.MaxGroundDistance - _behaviour.CollisionData.GroundDistance) / deltaTime; //ths results for smooth move on slopes                
            }
            else
                velocity.y -= Gravity * deltaTime;

            _mover.Velocity = velocity;
        }

        public override void LateUpdate(float deltaTime) { }


        public override void Exit()
        {
            UnsubscribeToKlonoaEvent();
        }

        public virtual void DirectionAction(Vector2 input)
        {
            _inputDirection = input;
        }

        public abstract void JumpAction();

        public abstract void JumpKeepAction();

        public abstract void JumpReleaseAction();

        public abstract void AttackAction();

        private void SubscribeToKlonoaEvent()
        {
            _behaviour.DirectionChangeEvent += DirectionAction;
            _behaviour.JumpInputEvent += JumpAction;
            _behaviour.JumpKeepInputEvent += JumpKeepAction;
            _behaviour.JumpReleaseInputEvent += JumpReleaseAction;
            _behaviour.AttackInputEvent += AttackAction;
        }

        private void UnsubscribeToKlonoaEvent()
        {
            _behaviour.DirectionChangeEvent -= DirectionAction;
            _behaviour.JumpInputEvent -= JumpAction;
            _behaviour.JumpKeepInputEvent -= JumpKeepAction;
            _behaviour.JumpReleaseInputEvent -= JumpReleaseAction;
            _behaviour.AttackInputEvent -= AttackAction;
        }

        public override void DrawGizmos() { }
    }
}