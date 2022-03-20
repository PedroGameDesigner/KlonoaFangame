using PlatformerRails;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    [System.Serializable]
    public class KlonoaStateOld
    {
        private readonly SpeedData _moveSpeed;
        private readonly float _exitTime;
        private readonly float _gravity;
        private readonly bool _canTurn;

        public delegate void SimpleAction();
        public delegate void ContinousAction(float deltaTime);
        private readonly SimpleAction _jumpAction;
        private readonly SimpleAction _jumpKeepAction;
        private readonly SimpleAction _jumpReleaseAction;
        private readonly SimpleAction _attackAction;
        private readonly SimpleAction _exitAction;
        private readonly ContinousAction _passiveAction;

        private float _timer;
        private bool _timerFinished;
        private Vector2 _lastDirection;

        public KlonoaStateOld(SpeedData moveSpeed = null, float exitTime = -1, 
                            float gravity = 0, bool canTurn = false,
                            SimpleAction jumpAction = null, SimpleAction jumpKeepAction = null,
                            SimpleAction jumpReleaseAction = null, SimpleAction attackAction = null,
                            SimpleAction exitAction = null, ContinousAction passiveAction = null)
        {
            _moveSpeed = moveSpeed;
            _exitTime = exitTime;
            _gravity = gravity;
            _canTurn = canTurn;

            _jumpAction = jumpAction;
            _jumpKeepAction = jumpKeepAction;
            _jumpReleaseAction = jumpReleaseAction;
            _attackAction = attackAction;
            _passiveAction = passiveAction;

            _exitAction = exitAction;
        }
        public void Restart()
        {
            _timer = 0;
            _timerFinished = false;
    }

        public void FixedUpdate(MoverOnRails mover, Vector2 input, CollisionData collision, float deltaTime)
        {
            UpdateMove(mover, input, deltaTime);
            UpdateGravity(mover, collision, deltaTime);
            UpdateTimer(deltaTime);
            _passiveAction?.Invoke(deltaTime);
        }

        private void UpdateGravity(MoverOnRails mover, CollisionData collision, float deltaTime)
        {
            if (_gravity <= 0) return;
            //Y+ axis = Upwoard (depends on rail rotation)
            Vector3 velocity = mover.Velocity;
            if (collision.Grounded)
            {
                velocity.y = (collision.MaxGroundDistance - collision.GroundDistance) / deltaTime; //ths results for smooth move on slopes                
            }
            else
                velocity.y -= _gravity * deltaTime;

            mover.Velocity = velocity;
        }

        private void UpdateMove(MoverOnRails mover, Vector2 input, float deltaTime)
        {
            if (_moveSpeed.Acceleration <= 0) return;
            //Changing Z value in local position means moving toward rail direction
            Vector3 velocity = mover.Velocity;
            if (_canTurn)
                _lastDirection = input;
            velocity.z += _lastDirection.x * _moveSpeed.Acceleration * deltaTime;
            velocity.z -= mover.Velocity.z * _moveSpeed.Drag * deltaTime;
            mover.Velocity = velocity;
        }

        private void UpdateTimer(float deltaTime)
        {
            if (_timerFinished || _exitTime < 0) return;
            
            _timer += deltaTime;
            if (_timer > _exitTime)
            {
                _exitAction?.Invoke();
                _timerFinished = true;
            }            
        }

        public void JumpAction()
        {
            _jumpAction?.Invoke();
        }

        public void JumpKeepAction()
        {
            _jumpKeepAction?.Invoke();
        }

        public void JumpReleaseAction()
        {
            _jumpReleaseAction?.Invoke();
        }

        public void AttackAction()
        {
            _attackAction?.Invoke();
        }
    }
}