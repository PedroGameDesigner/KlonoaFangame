using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace Gameplay.Enemies.Ball
{
    [RequireComponent(typeof(EnemyBall))]
    public class EnemyBallSM : StateMachineMono<EnemyBall>
    {
        protected HoldedState _holdedState;
        protected PathFlyState _pathFlyState;
        protected FreeFlyState _freeFlyState;

        protected override State<EnemyBall> InitializeStates()
        {
            _freeFlyState = new FreeFlyState(_behaviour);
            _pathFlyState = new PathFlyState(_behaviour, _freeFlyState);
            _holdedState = new HoldedState(_behaviour, _pathFlyState);
            return _holdedState;
        }
    }
}
