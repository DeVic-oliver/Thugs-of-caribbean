namespace Assets.Scripts.GameManager.StateMachine
{
    using System.Collections.Generic;
    using Assets.Scripts.Core.Components.Counters;
    using Assets.Scripts.Player;
    using Devic.Scripts.Utils.StateMachine;
    using UnityEngine;
    using UnityEngine.InputSystem;

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
        [SerializeField] private TimerCounter _gameTimer;
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerInput _inputSystem;
        #endregion
        protected override Dictionary<string, IConcreteState> RegisterConcreteStates()
        {
            Dictionary<string, IConcreteState> statesRegistered = new();
            statesRegistered.Add("START", StartState = new StartState(_gameTimer, _scoreCounter));
            statesRegistered.Add("GAMEPLAY", Gameplay = new GamePlayState(_gameTimer, _playerHealth, _inputSystem));
            statesRegistered.Add("PAUSE", Pause = new PauseState(_inputSystem));
            statesRegistered.Add("GAMEOVER", Gameover = new GameOverState());

            return statesRegistered;
        }

        protected override IConcreteState SetInitialState()
        {
            return StartState;
        }
        
    }
}
