using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.StateMachine
{
    using Devic.Scripts.Utils.StateMachine;
    using Assets.Scripts.Player.StateMachine.ConcreteStates;
    
    public class PlayerStateMachine : StateMachine
    {
        [SerializeField] private Animator _animator;

        #region STATES
        private enum statesID
        {
            IDLE,
            MOVE,
            RUN,
            DEAD
        }

        private IConcreteState _idleState;
        private IConcreteState _moveState;
        #endregion


        protected override IConcreteState SetInitialState()
        {
            return _idleState;
        }

        protected override Dictionary<string, IConcreteState> RegisterConcreteStates()
        {
            InitObjects();
            var registeredStates = new Dictionary<string, IConcreteState>();
            registeredStates.Add(statesID.IDLE.ToString(), _idleState);
            registeredStates.Add(statesID.MOVE.ToString(), _moveState);

            return registeredStates;
        }
        private void InitObjects()
        {
            var playerMovment = GetComponent<PlayerMovment>();
            
            _idleState = new IdleState(_animator, playerMovment);
            _moveState = new MoveState(_animator, playerMovment);
        }

        private new void Start()
        {
            base.Start();
        }

        private new void Update()
        {
            base.Update();
        }
    }
}
