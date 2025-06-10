public class StateMachine
{
    public IROGState currentState { get; private set; }

    public void ChangeState(IROGState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState?.OnEnter();
    }

    public void Update()
    {
        currentState?.OnUpdate();
    }
}
