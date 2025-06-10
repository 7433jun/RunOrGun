using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private string currentState;

    public StateMachine StateMachine { get; private set; }
    public IROGState PlayerSpawnState { get; private set; }
    public IROGState PlayerIdleState { get; private set; }
    public IROGState PlayerMoveState { get; private set; }
    public IROGState PlayerAttackState { get; private set; }
    public IROGState PlayerDieState { get; private set; }

    public void Initialize(Player player)
    {
        StateMachine = new StateMachine();
        PlayerSpawnState = new PlayerSpawnState(player, this);
        PlayerIdleState = new PlayerIdleState(player, this);
        PlayerMoveState = new PlayerMoveState(player, this);
        PlayerAttackState = new PlayerAttackState(player, this);
        PlayerDieState = new PlayerDieState(player, this);

        StateMachine.ChangeState(PlayerSpawnState);
    }

    void Update()
    {
        currentState = StateMachine.currentState.ToString();

        StateMachine.Update();
    }
}
