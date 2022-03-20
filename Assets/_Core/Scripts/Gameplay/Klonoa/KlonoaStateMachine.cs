using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace Gameplay.Klonoa
{
    public class KlonoaStateMachine : StateMachine<KlonoaBehaviour>
    {
        private NormalState _normalState;
        private FloatState _floatState;
        private CaptureState _captureState;
        private HoldingState _holdingState;
        private KlonoaState _doubleJump;

        public bool IsFloatState => _currentState == _floatState;

        public KlonoaStateMachine(KlonoaBehaviour behaviour) : base(behaviour) { }

        protected override State<KlonoaBehaviour> InitializeStates()
        {
            _normalState = new NormalState(_behaviour);
            _floatState = new FloatState(_behaviour);
            _captureState = new CaptureState(_behaviour);
            _holdingState = new HoldingState(_behaviour);

            _normalState.SetStates(_floatState, _captureState);
            _floatState.SetStates(_normalState);
            _captureState.SetStates(_normalState, _holdingState);
            _holdingState.SetStates(_normalState);

            return _normalState;
        }

        protected override void OnStateChange(State<KlonoaBehaviour> nextState)
        {
            if (_currentState != null)
                Debug.LogFormat("Klonoa state change: {0} => {1}", _currentState.GetType(), nextState.GetType());
            else
                Debug.LogFormat("Klonoa first state: {0}", nextState.GetType());

            base.OnStateChange(nextState);
        }
    }
}