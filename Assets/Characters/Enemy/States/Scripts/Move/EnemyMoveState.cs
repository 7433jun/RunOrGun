using UnityEngine;

public class EnemyMoveState : IROGState
{
    private Enemy enemy;
    private EnemyStateMachine enemyStateMachine;
    private EnemyMoveBehavior move;

    public EnemyMoveState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
        move = enemy.GetComponent<EnemyMoveBehavior>();
        move.InitBehavior(enemy);
    }

    public void OnEnter()
    {


        move.EnterBehavior();
    }

    public void OnUpdate()
    {
        if (enemy.characterRegistry.Player == null)
        {
            enemyStateMachine.StateMachine.ChangeState(enemyStateMachine.EnemyIdleState);
        }

        move.UpdateBehavior();
    }

    public void OnExit()
    {


        move.ExitBehavior();
    }
}
