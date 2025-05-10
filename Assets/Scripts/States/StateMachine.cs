using UnityEngine;

public interface IROGState
{
    void OnEnter();
    void OnUpdate();
    void OnExit();
}

public class StateMachine
{
    private IROGState currentState;

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
