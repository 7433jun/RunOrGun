using UnityEngine;

public class EnemyIdleState : IROGState
{
    private Enemy enemy;
    private EnemyStateMachine enemyStateMachine;
    private EnemyIdleBehavior idle;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
        idle = enemy.GetComponent<EnemyIdleBehavior>();
        idle.InitBehavior(enemy);
    }

    public void OnEnter()
    {


        idle.EnterBehavior();
    }

    public void OnUpdate()
    {
        if (enemy.characterRegistry.Player != null)
        {
            enemyStateMachine.StateMachine.ChangeState(enemyStateMachine.EnemyMoveState);
        }

        idle.UpdateBehavior();
    }

    public void OnExit()
    {


        idle.ExitBehavior();
    }
}
