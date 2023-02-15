namespace Assets.Scripts.GameManager.StateMachine
{
    using Devic.Scripts.Utils.StateMachine;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;
    public class PauseState : IConcreteState
    {
        private PlayerInput _inputSystem;
        private InputAction _pauseAction;
        private Button _resumeButton;
        private Button _exitButton;
        private GameObject _pauseMenu;
        private bool _goToGameplayState;

        public PauseState(PlayerInput inputSystem, GameObject pauseMenu, Button resumeButton, Button exitButton)
        {
            _inputSystem = inputSystem;
            _pauseAction = _inputSystem.actions.FindAction("Pause");
            _resumeButton = resumeButton;
            _pauseMenu = pauseMenu;
            _resumeButton.onClick.AddListener(ResumeGame);
            _exitButton = exitButton;
            _exitButton.onClick.AddListener(ExitGame);
        }
        private void ResumeGame()
        {
            _goToGameplayState = true;
        }
        private void ExitGame()
        {
            Debug.Log("EXITING");
            ///LOAD MAIN MENU SCENE
            //var mainMenu = SceneManager.GetSceneByBuildIndex(0);
            //SceneManager.LoadScene(mainMenu.buildIndex);
        }

        public void OnStateEnter(StateMachine stateMachine)
        {
            DisableAllActionsExceptPause();
            _pauseMenu.SetActive(true);
            PauseGame();
            _goToGameplayState = false;
        }
        private void DisableAllActionsExceptPause()
        {
            foreach (var item in _inputSystem.actions)
            {
                if (item.name != "Pause")
                {
                    item.Disable();
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
            ChangeToPlayStateWhenPauseActionIsPerformed(stateMachine);
            ChangeToPlayStateIfResumeButtonClicked(stateMachine);
        }
        private void ChangeToPlayStateWhenPauseActionIsPerformed(StateMachine stateMachine)
        {
            _pauseAction.performed += ctx => stateMachine.SwitchState("GAMEPLAY");
        }
        private void ChangeToPlayStateIfResumeButtonClicked(StateMachine stateMachine)
        {
            if (_goToGameplayState)
            {
                stateMachine.SwitchState("GAMEPLAY");
            }
        }
    }
}