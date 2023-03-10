namespace Assets.Scripts.GameManager.StateMachine
{
    using Devic.Scripts.Utils.StateMachine;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using Assets.Scripts.Core.Components.Audio;
    using System.Collections;

    public class PauseState : IConcreteState
    {
        private StateMachine _machine;
        private PlayerInput _inputSystem;
        private InputAction _pauseAction;
        #region buttons
        private Button _resumeButton;
        private Button _controlsButton;
        private Button _exitButton;
        #endregion
        #region uiGameObjectes
        private GameObject _pauseMenu;
        private GameObject _controlsMural;
        #endregion
        private bool _canGoToGameplayState;
        private int _mainMenuSceneIndex = 0;
        private UIAudioManagerBase _uiAudioManager;

        public PauseState(PlayerInput inputSystem, GameObject pauseMenu, GameObject controlsMural, Button resumeButton, Button controlsButton, Button exitButton, UIAudioManagerBase uiAudioManager)
        {
            _inputSystem = inputSystem;
            _pauseAction = _inputSystem.actions.FindAction("Pause");
            _resumeButton = resumeButton;
            _pauseMenu = pauseMenu;
            _resumeButton.onClick.AddListener(ResumeGame);
            _exitButton = exitButton;
            _exitButton.onClick.AddListener(ExitGame);
            _controlsMural = controlsMural;
            _controlsButton = controlsButton;
            _controlsButton.onClick.AddListener(ShowControls);
            AllowGoToPlayStateByActionsPerformed();
            _uiAudioManager = uiAudioManager;
        }
        private void AllowGoToPlayStateByActionsPerformed()
        {
                _pauseAction.performed += ctx => _canGoToGameplayState = true;
        }

        private void ResumeGame()
        {
            _uiAudioManager.PlayButtonClick();
            _canGoToGameplayState = true;
        }
        private void ExitGame()
        {
            _uiAudioManager.PlayButtonClick();
            Time.timeScale = 1;
            SceneManager.LoadScene(_mainMenuSceneIndex);
        }
        private void ShowControls()
        {
            _uiAudioManager.PlayButtonClick();
            _controlsMural.SetActive(true);
        }

        public void OnStateEnter(StateMachine stateMachine)
        {
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
                _controlsMural.SetActive(false);
                _machine.SwitchState("GAMEPLAY");
            }
        }
    }
}