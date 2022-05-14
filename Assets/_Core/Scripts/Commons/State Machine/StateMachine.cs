using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine<E>
    {
        protected E _behaviour;
        protected State<E> _currentState;

        public event Action StateChangeEvent;

        protected StateMachine(E behaviour)
        {
            _behaviour = behaviour;
        }

        public void StartMachine()
        {
            State<E> firstState = InitializeStates();
            OnStateChange(firstState);
        }

        protected abstract State<E> InitializeStates();

        public void Update(float deltaTime)
        {
            _currentState?.Update(deltaTime);
        }

        public void FixedUpdate(float deltaTime)
        {
            _currentState?.FixedUpdate(deltaTime);
        }

        public void LateUpdate(float deltaTime)
        {
            _currentState?.LateUpdate(deltaTime);
        }

        protected virtual void OnStateChange(State<E> nextState)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
                _currentState.StateChangeEvent -= OnStateChange;
            }

            _currentState = nextState;
            if (_currentState != null)
            {
                _currentState.StateChangeEvent += OnStateChange;
                _currentState.Enter();
                StateChangeEvent?.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            _currentState?.DrawGizmos();
        }
    }
}
