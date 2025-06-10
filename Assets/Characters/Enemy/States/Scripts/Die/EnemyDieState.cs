using UnityEngine;

public class EnemyDieState : IROGState
{
    private Enemy enemy;
    private EnemyStateMachine enemyStateMachine;
    private EnemyDieBehavior die;

    public EnemyDieState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
        die = enemy.GetComponent<EnemyDieBehavior>();
        die.InitBehavior(enemy);
    }

    public void OnEnter()
    {


        die.EnterBehavior();
    }

    public void OnUpdate()
    {


        die.UpdateBehavior();
    }

    public void OnExit()
    {


        die.ExitBehavior();
    }
}
