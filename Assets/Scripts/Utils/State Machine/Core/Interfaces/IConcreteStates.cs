namespace Devic.Scripts.Utils.StateMachine
{
    public interface IConcreteState
    {
        public void OnStateEnter(StateMachine stateMachine);
        public void OnUpdateState(StateMachine stateMachine);
    }
}