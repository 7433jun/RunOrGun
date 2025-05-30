using UnityEngine;

public class EnemyIdleState : IROGState
{
    private Enemy enemy;

    public EnemyIdleState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {

    }

    public void OnUpdate()
    {
        if (enemy.characterRegistry.Player != null)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyMoveState);
        }
    }

    public void OnExit()
    {

    }
}
