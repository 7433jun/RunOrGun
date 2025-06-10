using UnityEngine;

public class EnemySpawnState : IROGState
{
    private Enemy enemy;
    private EnemyStateMachine enemyStateMachine;
    private EnemySpawnBehavior spawn;

    public EnemySpawnState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
        spawn = enemy.GetComponent<EnemySpawnBehavior>();
        spawn.InitBehavior(enemy);
    }

    public void OnEnter()
    {


        spawn.EnterBehavior();
    }

    public void OnUpdate()
    {
        enemyStateMachine.StateMachine.ChangeState(enemyStateMachine.EnemyIdleState);

        spawn.UpdateBehavior();
    }

    public void OnExit()
    {


        spawn.ExitBehavior();
    }
}
