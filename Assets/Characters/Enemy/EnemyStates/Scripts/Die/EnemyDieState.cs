using UnityEngine;

public class EnemyDieState : IROGState
{
    private Enemy enemy;
    private EnemyDieBehaviorSO dieSO;

    public EnemyDieState(Enemy enemy)
    {
        this.enemy = enemy;
        dieSO = Object.Instantiate(enemy.DefinitionSO.DieSO);
        dieSO.InitBehavior(enemy);
    }

    public void OnEnter()
    {


        dieSO.EnterBehavior();
    }

    public void OnUpdate()
    {


        dieSO.UpdateBehavior();
    }

    public void OnExit()
    {


        dieSO.ExitBehavior();
    }
}
