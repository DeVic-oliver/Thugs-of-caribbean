﻿namespace Assets.Scripts.GameManager.StateMachine
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
        private GameObject _pauseMenu;
        private bool _goToGameplayState;

        public PauseState(PlayerInput inputSystem, GameObject pauseMenu, Button resumeButton)
        {
            _inputSystem = inputSystem;
            _pauseAction = _inputSystem.actions.FindAction("Pause");
            _resumeButton = resumeButton;
            _pauseMenu = pauseMenu;
            _resumeButton.onClick.AddListener(ResumeGame);
        }
        private void ResumeGame()
        {
            _goToGameplayState = true;
        }

        public void OnStateEnter(StateMachine stateMachine)
        {
            _pauseMenu.SetActive(true);
            PauseGame();
            _goToGameplayState = false;
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