using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine<E> : MonoBehaviour 
    {
        protected E _behaviour;
        protected State<E> _currentState;

        private void Awake()
        {
            _behaviour = GetComponent<E>();
            State<E> firstState = InitializeStates();
            OnStateChange(firstState);
        }

        protected abstract State<E> InitializeStates();

        private void Update()
        {
            _currentState?.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _currentState?.FixedUpdate(Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            _currentState?.LateUpdate(Time.deltaTime);
        }

        private void OnStateChange(State<E> nextState)
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
            }
        }

        private void OnDrawGizmos()
        {
            _currentState?.DrawGizmos();
        }
    }
}
