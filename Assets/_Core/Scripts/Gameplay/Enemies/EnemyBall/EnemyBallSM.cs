using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;

namespace Gameplay.Enemies.Ball
{
    [RequireComponent(typeof(EnemyBall))]
    public class EnemyBallSM : StateMachineMono<EnemyBall>
    {
        protected HoldedState _holdedState;
        protected TransitionState _transitionState;
        protected FreeFlyState _fullFreeFlyState;
        protected PathFlyState _pathFlyState;
        protected FreeFlyState _freeFlyState;
        protected DestroyedState _destroyedState;

        protected override State<EnemyBall> InitializeStates()
        {
            _fullFreeFlyState = new FreeFlyState(_behaviour, _behaviour.FullFlyTime);
            _transitionState = new TransitionState(_behaviour);
            _freeFlyState = new FreeFlyState(_behaviour, _behaviour.FreeFlyTime);
            _pathFlyState = new PathFlyState(_behaviour, _freeFlyState, _behaviour.FollowPathTime);
            _holdedState = new HoldedState(_behaviour, _pathFlyState, _fullFreeFlyState);
            _destroyedState = new DestroyedState(_behaviour);
            return _holdedState;
        }

        protected void Start()
        {
            _behaviour.StartTransitionEvent += OnStartTransition;
            _behaviour.DestroyEvent += OnDestroyEvent;
        }

        private void OnDestroyEvent()
        {
            OnStateChange(_destroyedState);
        }

        protected void OnStartTransition()
        {
            _transitionState.SetNextState(_currentState);
            OnStateChange(_transitionState);
        }

    }
}
