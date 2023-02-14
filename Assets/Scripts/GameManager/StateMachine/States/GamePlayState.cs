namespace Assets.Scripts.GameManager.StateMachine
{
    using Assets.Scripts.Core.Components.Counters;
    using Assets.Scripts.Player;
    using Devic.Scripts.Utils.StateMachine;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class GamePlayState : IConcreteState
    {
        private TimerCounter _gameTimer;
        private PlayerHealth _playerHealth;
        private PlayerInput _inputSystem;

        private InputAction _pauseAction;

        public GamePlayState(TimerCounter gameTimer, PlayerHealth playerHealth, PlayerInput inputSystem)
        {
            _gameTimer = gameTimer;
            _playerHealth = playerHealth;
            _inputSystem = inputSystem;
            _pauseAction = _inputSystem.actions.FindAction("Pause");
        }
        public void OnStateEnter(StateMachine stateMachine)
        {
            ResumeGame();
        }
        private void ResumeGame()
        {
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }

        public void OnUpdateState(StateMachine stateMachine)
        {
            ChangeToPauseStateWhenPauseActionIsPerformed(stateMachine);
            ChangeToGameOverWhenPlayerDiesOrTimeEnds(stateMachine);
        }
        private void ChangeToPauseStateWhenPauseActionIsPerformed(StateMachine stateMachine)
        {
            _pauseAction.performed += ctx => stateMachine.SwitchState("PAUSE");
        }
        private void ChangeToGameOverWhenPlayerDiesOrTimeEnds(StateMachine stateMachine)
        {
            if(_playerHealth.HasDied || _gameTimer.HasTimerReachedZero)
            {
                stateMachine.SwitchState("GAMEOVER");
            }
        }

    }
}