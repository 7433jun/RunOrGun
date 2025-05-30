using UnityEngine;

public class EnemySpawnState : IROGState
{
    private Enemy enemy;

    public EnemySpawnState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        enemy.characterRegistry.Register(enemy);

        enemy.StateMachine.ChangeState(enemy.EnemyIdleState);
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}
