namespace Assets.Scripts.GameManager.StateMachine
{
    using Assets.Scripts.Core.Components.Counters;
    using Devic.Scripts.Utils.StateMachine;
    using UnityEngine;

    public class StartState : IConcreteState
    {
        private TimerCounter _gameTimer;
        private ScoreCounter _scoreCounter;

        public StartState(TimerCounter gameTimer, ScoreCounter scoreCounter)
        {
            _gameTimer = gameTimer;
            _scoreCounter = scoreCounter;
        }
        public void OnStateEnter(StateMachine stateMachine)
        {
            Debug.Log("WELCOME TO START STATE");

            _gameTimer.StartTimer();
            _scoreCounter.ResetScore();
        }

        public void OnUpdateState(StateMachine stateMachine)
        {
            stateMachine.SwitchState("GAMEPLAY");
        }
    }
}