namespace Assets.Scripts.GameManager.StateMachine
{
    public abstract class GameplayConcreteState
    {
        protected GameplayStateMachine _stateMachine;


        public GameplayConcreteState(GameplayStateMachine stateMachine) 
        {
            _stateMachine = stateMachine;
        }

        public abstract void OnStateEnter();

        public abstract void OnUpdateState();
    }
}