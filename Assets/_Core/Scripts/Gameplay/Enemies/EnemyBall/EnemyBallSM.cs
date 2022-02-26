using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace Gameplay.Enemies.Ball
{
    [RequireComponent(typeof(EnemyBall))]
    public class EnemyBallSM : StateMachine<EnemyBall>
    {
        protected HoldedState _holdedState;
        protected FlyingState _flyingState;

        protected override State<EnemyBall> InitializeStates()
        {
            _flyingState = new FlyingState(_behaviour);
            _holdedState = new HoldedState(_behaviour, _flyingState);
            return _holdedState;
        }
    }
}
