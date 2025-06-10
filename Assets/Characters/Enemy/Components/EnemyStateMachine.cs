using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private string currentState;

    public StateMachine StateMachine { get; private set; }
    public IROGState EnemySpawnState { get; private set; }
    public IROGState EnemyIdleState { get; private set; }
    public IROGState EnemyMoveState { get; private set; }
    public IROGState EnemyAttackState { get; private set; }
    public IROGState EnemyDieState { get; private set; }

    public void Initialize(Enemy enemy)
    {
        StateMachine = new StateMachine();
        EnemySpawnState = new EnemySpawnState(enemy, this);
        EnemyIdleState = new EnemyIdleState(enemy, this);
        EnemyMoveState = new EnemyMoveState(enemy, this);
        EnemyAttackState = new EnemyAttackState(enemy, this);
        EnemyDieState = new EnemyDieState(enemy, this);

        StateMachine.ChangeState(EnemySpawnState);
    }

    void Update()
    {
        currentState = StateMachine.currentState.ToString();

        StateMachine.Update();
    }
}
