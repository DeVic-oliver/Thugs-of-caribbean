namespace Assets.Scripts.GameManager.StateMachine
{
    using Assets.Scripts.Core.Components.Counters;
    using Assets.Scripts.Core.Components.Spawner;
    using Assets.Scripts.Player;
    using UnityEngine;

    public class StartState : GameplayConcreteState
    {
        private TimerCounter _gameTimer;
        private PlayerHealth _player;
        private EnemySpawner _enemySpawner;


        public StartState(GameplayStateMachine stateMachine, TimerCounter gameTimer, PlayerHealth player, EnemySpawner enemySpawner) : base(stateMachine)
        {
            _gameTimer = gameTimer;
            _player = player;
            _enemySpawner = enemySpawner;
        }

        public override void OnStateEnter()
        {
            Time.timeScale = 1;
            _gameTimer.StartTimer();
            ScoreCounter.ResetScore();
            _enemySpawner.StopSpawning();
            ResetPlayerData();
        }

        private void ResetPlayerData()
        {
            _player.ResetStatus();
            GetPlayerTransform().position = new Vector2(0, 0);
        }

        private Transform GetPlayerTransform()
        {
            return _player.transform;
        }

        public override void OnUpdateState()
        {
            _stateMachine.SwitchState(_stateMachine.Gameplay);
        }
    }
}