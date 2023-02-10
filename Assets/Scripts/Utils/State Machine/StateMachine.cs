using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Devic.Scripts.Utils.StateMachine
{
    /// <summary>
    /// Abstract state machine base class will be inherited to provide the state machine funcionality to game objects.
    /// </summary>
    public abstract class StateMachine : MonoBehaviour
    {
        private IConcreteState _currentState;
        /// <summary>
        /// Holds a mapping to all states of its state machine
        /// </summary>
        private Dictionary<string, IConcreteState> _statesDictionary = new Dictionary<string, IConcreteState>();
        
        /// <summary>
        /// Search for a state in its dictionary and switches if it find.
        /// </summary>
        /// <param name="nextStateID">The state id that you want to switch</param>
        public void SwitchState(string nextStateID)
        {
            var upperCaseString = nextStateID.ToUpper();
            if(_statesDictionary.ContainsKey(upperCaseString)) 
            {
                _currentState = _statesDictionary[upperCaseString];
                _currentState.OnStateEnter(this);
            }
        }

        protected void Start()
        {
            _statesDictionary = RegisterConcreteStates();
            _currentState = SetInitialState();
            _currentState.OnStateEnter(this);
        }
        /// <summary>
        /// Initializes the FIRST state of the state machine.
        /// </summary>
        /// <returns>Must return the state that implements the IConcrete interface</returns>
        protected abstract IConcreteState SetInitialState();
        /// <summary>
        /// Mapps the state with an id
        /// </summary>
        /// <returns></returns>
        protected abstract Dictionary<string, IConcreteState> RegisterConcreteStates();
        
        protected void Update()
        {
            _currentState.OnUpdateState(this);
        }
    }
}