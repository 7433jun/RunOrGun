using UnityEngine;

public class EnemyAttackState : IROGState
{
    private Enemy enemy;

    public EnemyAttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {

    }

    public void OnUpdate()
    {
        if (enemy.characterRegistry.Player == null)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyIdleState);
        }
    }

    public void OnExit()
    {

    }
}
