namespace Assets.Scripts.GameManager.StateMachine
{
    public abstract class GameplayConcreteState
    {
        public abstract void OnStateEnter(GameplayStateMachine GameplayStateMachine);

        public abstract void OnUpdateState(GameplayStateMachine GameplayStateMachine);
    }
}