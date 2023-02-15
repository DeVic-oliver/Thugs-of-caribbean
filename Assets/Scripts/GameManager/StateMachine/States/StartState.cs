namespace Assets.Scripts.GameManager.StateMachine
{
    using Assets.Scripts.Core.Components.Counters;
    using Devic.Scripts.Utils.StateMachine;
    using UnityEngine;

    public class StartState : IConcreteState
    {
        private TimerCounter _gameTimer;
        private ScoreCounter _scoreCounter;
        private GameObject _player;

        public StartState(TimerCounter gameTimer, ScoreCounter scoreCounter, GameObject player)
        {
            _gameTimer = gameTimer;
            _scoreCounter = scoreCounter;
            _player = player;
        }
        public void OnStateEnter(StateMachine stateMachine)
        {
            Debug.Log("WELCOME TO START STATE");
            Time.timeScale = 1;
            _gameTimer.StartTimer();
            _scoreCounter.ResetScore();
            ResetPlayerPosition();
        }
        private void ResetPlayerPosition()
        {
            _player.transform.position = new Vector2(0, 0);
        }

        public void OnUpdateState(StateMachine stateMachine)
        {
            stateMachine.SwitchState("GAMEPLAY");
        }
    }
}