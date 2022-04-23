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
        private DoubleJumpState _doubleJumpState;
        private DamageState _damageState;
        private DeathState _deathState;

        public bool IsFloatState => _currentState == _floatState;
        public bool IsDoubleJumpState => _currentState == _doubleJumpState;
        public bool IsDamageState => _currentState == _damageState;

        public KlonoaStateMachine(KlonoaBehaviour behaviour) : base(behaviour) { }

        protected override State<KlonoaBehaviour> InitializeStates()
        {
            _normalState = new NormalState(_behaviour);
            _floatState = new FloatState(_behaviour);
            _captureState = new CaptureState(_behaviour);
            _holdingState = new HoldingState(_behaviour);
            _doubleJumpState = new DoubleJumpState(_behaviour);
            _damageState = new DamageState(_behaviour);
            _deathState = new DeathState(_behaviour);

            _normalState.SetStates(_floatState, _captureState);
            _floatState.SetStates(_normalState);
            _captureState.SetStates(_normalState, _holdingState);
            _holdingState.SetStates(_normalState, _doubleJumpState);
            _doubleJumpState.SetStates(_normalState);
            _damageState.SetStates(_normalState);

            return _normalState;
        }

        protected override void OnStateChange(State<KlonoaBehaviour> nextState)
        {
            if (_currentState != null)
                Debug.LogFormat("Klonoa: state change: {0} => {1}", _currentState.GetType(), nextState.GetType());
            else
                Debug.LogFormat("Klonoa: first state: {0}", nextState.GetType());

            base.OnStateChange(nextState);
        }

        public void ChangeToDamageState(RaycastHit hit)
        {
            OnStateChange(_damageState);
        }

        public void ChangeToDeathState()
        {
            OnStateChange(_deathState);
        }
    }
}