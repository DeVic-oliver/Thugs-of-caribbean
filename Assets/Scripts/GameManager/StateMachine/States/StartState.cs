namespace Assets.Scripts.GameManager.StateMachine
{
    using Assets.Scripts.Core.Components.Counters;
    using Assets.Scripts.Core.Components.Spawner;
    using Assets.Scripts.Player;
    using UnityEngine;

    public class StartState : GameplayConcreteState
    {
        private TimerCounter _gameTimer;
        private ScoreCounter _scoreCounter;
        private PlayerHealth _player;
        private EnemySpawner _enemySpawner;

        public StartState(TimerCounter gameTimer, ScoreCounter scoreCounter, PlayerHealth player, EnemySpawner enemySpawner)
        {
            _gameTimer = gameTimer;
            _scoreCounter = scoreCounter;
            _player = player;
            _enemySpawner = enemySpawner;
        }
        public override void OnStateEnter(GameplayStateMachine GameplayStateMachine)
        {
            Time.timeScale = 1;
            _gameTimer.StartTimer();
            _scoreCounter.ResetScore();
            ResetPlayerData();
            _enemySpawner.StopSpawning();
        }
        private void ResetPlayerData()
        {
            _player.ResetStatus();
            _player.transform.position = new Vector2(0, 0);
        }

        public override void OnUpdateState(GameplayStateMachine GameplayStateMachine)
        {
            //GameplayStateMachine.SwitchState("GAMEPLAY");
        }
    }
}