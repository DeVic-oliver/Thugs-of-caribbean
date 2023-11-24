namespace Assets.Scripts.GameManager.StateMachine
{
    using Assets.Scripts.Core.Components.Audio;
    using Assets.Scripts.Core.Components.Counters;
    using Assets.Scripts.Core.Components.Spawner;
    using Assets.Scripts.Player;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;

    public class GameplayStateMachine : MonoBehaviour
    {
        #region STATES
        public StartState StartState { get; private set; }
        public GameplayState Gameplay { get; private set; }
        public PauseState Pause { get; private set; }
        public GameoverState Gameover { get; private set; }

        private GameplayConcreteState _currentState;
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
        [SerializeField] private GameObject _controlsMural;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _pauseControlsButton;
        [SerializeField] private Button _pauseExitButton;

        [Header("Gameover State Dependencies")]
        [SerializeField] private GameObject _gameOverUI;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _gameOverExitButton;
        #endregion

        [Header("Audio")]
        [SerializeField] private UIAudioManagerBase _uiAudioManager;


        public void SwitchState(GameplayConcreteState newState)
        {
            _currentState = newState;
            _currentState.OnStateEnter();
        }

        private void Start()
        {
            InitStates();
            _currentState = StartState;
            _currentState.OnStateEnter();
        }

        private void InitStates()
        {
            StartState = new StartState(this, _gameTimer, _scoreCounter, _playerHealth, _enemySpawner);
            Gameplay = new GameplayState(this, _gameTimer, _playerHealth, _playerInputSystem, _pauseMenu, _enemySpawner);
            Pause = new PauseState(this, _playerInputSystem, _pauseMenu, _controlsMural, _resumeButton, _pauseControlsButton, _gameOverExitButton, _uiAudioManager);
            Gameover = new GameoverState(this, _gameOverUI, _restartButton, _pauseExitButton, _uiAudioManager);
        }

        private void Update()
        {
            _currentState.OnUpdateState();
        }
    }
}
