using PlatformerRails;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Klonoa
{
    public class KlonoaState
    {
        private SpeedData _moveSpeed = null;
        private float _groundDistance = 0.5f;
        private float _gravity = 0;
        private bool _canTurn = false;

        public delegate void SimpleAction();
        public delegate void ContinousAction(float deltaTime);
        private SimpleAction _jumpAction;
        private SimpleAction _jumpKeepAction;
        private SimpleAction _attackAction;
        private ContinousAction _passiveAction;

        private Vector2 _lastDirection;

        public KlonoaState(SpeedData moveSpeed = null, float groundDistance = 0, float gravity = 0, bool canTurn = false,
                            SimpleAction jumpAction = null, SimpleAction jumpKeepAction = null,
                            SimpleAction attackAction = null, ContinousAction passiveAction = null)
        {
            _moveSpeed = moveSpeed;
            _groundDistance = groundDistance;
            _gravity = gravity;
            _canTurn = canTurn;

            _jumpAction = jumpAction;
            _jumpKeepAction = jumpKeepAction;
            _attackAction = attackAction;
            _passiveAction = passiveAction;
        }

        public void FixedUpdate(MoverOnRails mover, Vector2 input, float deltaTime, float? floorDistance)
        {
            UpdateMove(mover, input, deltaTime);
            UpdateGravity(mover, deltaTime, floorDistance);
            _passiveAction?.Invoke(deltaTime);
        }

        private void UpdateGravity(MoverOnRails mover, float deltaTime, float? floorDistance)
        {
            if (_gravity <= 0) return;
            //Y+ axis = Upwoard (depends on rail rotation)
            if (floorDistance != null)
            {
                mover.Velocity.y = (_groundDistance - floorDistance.Value) / deltaTime; //ths results for smooth move on slopes                
            }
            else
                mover.Velocity.y -= _gravity * deltaTime;

        }

        private void UpdateMove(MoverOnRails mover, Vector2 input, float deltaTime)
        {
            if (_moveSpeed.Acceleration <= 0) return;
            //Changing Z value in local position means moving toward rail direction
            if (_canTurn)
                _lastDirection = input;
            mover.Velocity.z += _lastDirection.x * _moveSpeed.Acceleration * deltaTime;
            mover.Velocity.z -= mover.Velocity.z * _moveSpeed.Drag * deltaTime;
        }

        public void JumpAction()
        {
            _jumpAction?.Invoke();
        }
        public void JumpKeepAction()
        {
            _jumpKeepAction?.Invoke();
        }
        public void AttackAction()
        {
            _attackAction?.Invoke();
        }
    }
}