namespace Assets.Scripts.GameManager.StateMachine
{
    using Assets.Scripts.Core.Components.Counters;
    using Assets.Scripts.Core.Components.Spawner;
    using Assets.Scripts.Player;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class GameplayState : GameplayConcreteState
    {
        private TimerCounter _gameTimer;
        private PlayerHealth _playerHealth;
        private PlayerInput _inputSystem;
        private InputAction _pauseAction;
        private GameObject _pauseMenu;
        private EnemySpawner _enemySpawner;
        private bool _canGoToPauseState = false;

        public GameplayState(GameplayStateMachine stateMachine, TimerCounter gameTimer, PlayerHealth playerHealth, PlayerInput inputSystem, GameObject pauseMenu, EnemySpawner enemySpawner) : base(stateMachine)
        {
            _gameTimer = gameTimer;
            _playerHealth = playerHealth;
            _inputSystem = inputSystem;
            _pauseAction = inputSystem.actions.FindAction("Pause");
            _pauseMenu = pauseMenu;
            _enemySpawner = enemySpawner;
            AllowGoToPauseStateByActionsPerformed();
        }

        private void AllowGoToPauseStateByActionsPerformed()
        {
            _pauseAction.performed += ctx => _canGoToPauseState = true;
        }

        public override void OnStateEnter()
        {
            _canGoToPauseState = false;
            EnablePlayerInputActions();
            ResumeTimeScaleToOne();
            _pauseMenu.SetActive(false);
            _enemySpawner.StartSpawnObjects();
        }

        private void EnablePlayerInputActions()
        {
            foreach (var item in _inputSystem.actions)
                item.Enable();
        }

        private void ResumeTimeScaleToOne()
        {
            if(Time.timeScale == 0)
                Time.timeScale = 1;
        }

        public override void  OnUpdateState()
        {
            GoToPauseStateIfAllowed();
            ChangeToGameOverWhenPlayerDiesOrTimeEnds();
        }

        private void GoToPauseStateIfAllowed()
        {
            if (_canGoToPauseState)
                _stateMachine.SwitchState(_stateMachine.Pause);
        }
       
        private void ChangeToGameOverWhenPlayerDiesOrTimeEnds()
        {
            if(_playerHealth.HasJustDied || _gameTimer.HasTimerReachedZero)
                _stateMachine.SwitchState(_stateMachine.Gameover);
        }

    }
}