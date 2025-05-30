using UnityEngine;

public class EnemyMoveState : IROGState
{
    private Enemy enemy;

    public EnemyMoveState(Enemy enemy)
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
