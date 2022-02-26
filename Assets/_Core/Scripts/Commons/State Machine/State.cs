using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class State<E>
    {
        protected E _behaviour;

        public delegate void StateChangeDelegate(State<E> state);
        public event StateChangeDelegate StateChangeEvent;

        protected State(E behaviour)
        {
            _behaviour = behaviour;
        }

        public abstract void Enter();
        public abstract void Update(float deltaTime);
        public abstract void FixedUpdate(float deltaTime);
        public abstract void LateUpdate(float deltaTime);
        public abstract void Exit();
        public abstract void DrawGizmos();

        protected void ChangeState(State<E> nextState)
        {
            StateChangeEvent?.Invoke(nextState);
        }
    }
}