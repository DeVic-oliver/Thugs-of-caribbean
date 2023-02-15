namespace Assets.Scripts.GameManager.StateMachine
{
    using Devic.Scripts.Utils.StateMachine;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class PauseState : IConcreteState
    {
        private StateMachine _machine;
        private PlayerInput _inputSystem;
        private InputAction _pauseAction;
        private Button _resumeButton;
        private Button _exitButton;
        private GameObject _pauseMenu;
        private bool _canGoToGameplayState;
        private int _mainMenuSceneIndex = 0;

        public PauseState(PlayerInput inputSystem, GameObject pauseMenu, Button resumeButton, Button exitButton)
        {
            _inputSystem = inputSystem;
            _pauseAction = _inputSystem.actions.FindAction("Pause");
            _resumeButton = resumeButton;
            _pauseMenu = pauseMenu;
            _resumeButton.onClick.AddListener(ResumeGame);
            _exitButton = exitButton;
            _exitButton.onClick.AddListener(ExitGame);
            AllowGoToPlayStateByActionsPerformed();
        }
        private void AllowGoToPlayStateByActionsPerformed()
        {
            _pauseAction.performed += ctx => _canGoToGameplayState = true;
        }

        private void ResumeGame()
        {
            _canGoToGameplayState = true;
        }
        private void ExitGame()
        {
            SceneManager.LoadScene(_mainMenuSceneIndex);
        }

        public void OnStateEnter(StateMachine stateMachine)
        {
            Debug.Log("WELCOME TO PAUSE STATE");
            InitStateMachineIfItsNull(stateMachine);
            _canGoToGameplayState = false;
            PauseGame();
            DisableAllActionsExceptPause();
            _pauseMenu.SetActive(true);
        }
        private void InitStateMachineIfItsNull(StateMachine stateMachine)
        {
            if (_machine == null)
            {
                _machine = stateMachine;
            }
        }
        private void DisableAllActionsExceptPause()
        {
            foreach (var action in _inputSystem.actions)
            {
                if (action.name != "Pause")
                {
                    action.Disable();
                }
            }
        }
        private void PauseGame()
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
        }

        public void OnUpdateState(StateMachine stateMachine)
        {
            GoToPlayStateIfItsAllowed();
        }
        private void GoToPlayStateIfItsAllowed()
        {
            if (_canGoToGameplayState)
            {
                _machine.SwitchState("GAMEPLAY");
            }
        }
    }
}