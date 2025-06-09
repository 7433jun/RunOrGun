using UnityEngine;

public class EnemyMoveState : IROGState
{
    private Enemy enemy;
    private EnemyMoveBehaviorSO moveSO;

    public EnemyMoveState(Enemy enemy)
    {
        this.enemy = enemy;
        moveSO = Object.Instantiate(enemy.DefinitionSO.MoveSO);
        moveSO.InitBehavior(enemy);
    }

    public void OnEnter()
    {


        moveSO.EnterBehavior();
    }

    public void OnUpdate()
    {
        if (enemy.characterRegistry.Player == null)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyIdleState);
        }

        moveSO.UpdateBehavior();
    }

    public void OnExit()
    {


        moveSO.ExitBehavior();
    }
}
