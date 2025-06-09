using UnityEngine;

public class EnemySpawnState : IROGState
{
    private Enemy enemy;
    private EnemySpawnBehaviorSO spawnSO;

    public EnemySpawnState(Enemy enemy)
    {
        this.enemy = enemy;
        spawnSO = Object.Instantiate(enemy.DefinitionSO.SpawnSO);
        spawnSO.InitBehavior(enemy);
    }

    public void OnEnter()
    {


        spawnSO.EnterBehavior();
    }

    public void OnUpdate()
    {
        enemy.StateMachine.ChangeState(enemy.EnemyIdleState);

        spawnSO.UpdateBehavior();
    }

    public void OnExit()
    {


        spawnSO.ExitBehavior();
    }
}
