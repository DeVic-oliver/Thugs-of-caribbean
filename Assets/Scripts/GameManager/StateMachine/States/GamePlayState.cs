namespace Assets.Scripts.GameManager.StateMachine
{
    using Assets.Scripts.Core.Components.Counters;
    using Assets.Scripts.Core.Components.Spawner;
    using Assets.Scripts.Player;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class GameplayState : GameplayConcreteState
    {
        private GameplayStateMachine _machine;
        private TimerCounter _gameTimer;
        private PlayerHealth _playerHealth;
        private PlayerInput _inputSystem;
        private InputAction _pauseAction;
        private GameObject _pauseMenu;
        private EnemySpawner _enemySpawner;
        private bool _canGoToPauseState = false;

        public GameplayState(TimerCounter gameTimer, PlayerHealth playerHealth, PlayerInput inputSystem, GameObject pauseMenu, EnemySpawner enemySpawner)
        {
            _gameTimer = gameTimer;
            _playerHealth = playerHealth;
            _inputSystem = inputSystem;
            _pauseAction = _inputSystem.actions.FindAction("Pause");
            _pauseMenu = pauseMenu;
            _enemySpawner = enemySpawner;
            AllowGoToPauseStateByActionsPerformed();
        }
        private void AllowGoToPauseStateByActionsPerformed()
        {
            _pauseAction.performed += ctx => _canGoToPauseState = true;
        }

        public override void OnStateEnter(GameplayStateMachine GameplayStateMachine)
        {
            _canGoToPauseState = false;
            EnablePlayerInputActions();
            ResumeGame();
            _pauseMenu.SetActive(false);
            _enemySpawner.StartSpawnObjects();
        }

        private void EnablePlayerInputActions()
        {
            foreach (var item in _inputSystem.actions)
            {
                    item.Enable();
            }
        }
        private void ResumeGame()
        {
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }

        public override void  OnUpdateState(GameplayStateMachine GameplayStateMachine)
        {
            GoToPauseStateIfItsAllowed();
            ChangeToGameOverWhenPlayerDiesOrTimeEnds();
        }
        private void GoToPauseStateIfItsAllowed()
        {
            if (_canGoToPauseState)
            {
                //_machine.SwitchState("PAUSE");
            }
        }
       
        private void ChangeToGameOverWhenPlayerDiesOrTimeEnds()
        {
            if(_playerHealth.HasJustDied || _gameTimer.HasTimerReachedZero)
            {
                //_machine.SwitchState("GAMEOVER");
            }
        }

    }
}