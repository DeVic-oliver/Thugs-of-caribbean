namespace Assets.Scripts.GameManager.StateMachine
{
    using System.Collections.Generic;
    using Assets.Scripts.Core.Components.Counters;
    using Assets.Scripts.Core.Components.Spawner;
    using Assets.Scripts.Player;
    using Devic.Scripts.Utils.StateMachine;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;
    public class GMStateMachine : StateMachine
    {
        #region STATES
        public IConcreteState StartState;
        public IConcreteState Gameplay;
        public IConcreteState Pause;
        public IConcreteState Gameover;
        #endregion

        #region COMPONENTS DEPENDENCY
        [Header("Rquired Components Setup")]
        [Space(10)]
        [SerializeField] private TimerCounter _gameTimer;
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerInput _playerInputSystem;
        [SerializeField] private EnemySpawner _enemySpawner;

        [Header("Pause State Dependencies")]
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _pauseExitButton;

        [Header("Gameover State Dependencies")]
        [SerializeField] private GameObject _gameOverUI;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _gameOverExitButton;
        #endregion

        protected override Dictionary<string, IConcreteState> RegisterConcreteStates()
        {
            Dictionary<string, IConcreteState> statesRegistered = new();
            statesRegistered.Add("START", StartState = new StartState(_gameTimer, _scoreCounter, _playerHealth, _enemySpawner));
            statesRegistered.Add("GAMEPLAY", Gameplay = new GamePlayState(_gameTimer, _playerHealth, _playerInputSystem, _pauseMenu, _enemySpawner));
            statesRegistered.Add("PAUSE", Pause = new PauseState(_playerInputSystem, _pauseMenu, _resumeButton, _pauseExitButton));
            statesRegistered.Add("GAMEOVER", Gameover = new GameOverState(_gameOverUI, _restartButton, _gameOverExitButton));

            return statesRegistered;
        }

        protected override IConcreteState SetInitialState()
        {
            return StartState;
        }
        
    }
}
