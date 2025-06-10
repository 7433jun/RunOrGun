using UnityEngine;

public class EnemyAttackState : IROGState
{
    private Enemy enemy;
    private EnemyStateMachine enemyStateMachine;
    private EnemyAttackBehavior attack;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
        attack = enemy.GetComponent<EnemyAttackBehavior>();
        attack.InitBehavior(enemy);
    }

    public void OnEnter()
    {


        attack.EnterBehavior();
    }

    public void OnUpdate()
    {
        if (enemy.characterRegistry.Player == null)
        {
            enemyStateMachine.StateMachine.ChangeState(enemyStateMachine.EnemyIdleState);
        }

        attack.UpdateBehavior();
    }

    public void OnExit()
    {


        attack.ExitBehavior();
    }
}
