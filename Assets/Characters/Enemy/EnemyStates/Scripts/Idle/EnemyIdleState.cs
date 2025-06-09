using UnityEngine;

public class EnemyIdleState : IROGState
{
    private Enemy enemy;
    private EnemyIdleBehaviorSO idleSO;

    public EnemyIdleState(Enemy enemy)
    {
        this.enemy = enemy;
        idleSO = Object.Instantiate(enemy.DefinitionSO.IdleSO);
        idleSO.InitBehavior(enemy);
    }

    public void OnEnter()
    {


        idleSO.EnterBehavior();
    }

    public void OnUpdate()
    {
        if (enemy.characterRegistry.Player != null)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyMoveState);
        }

        idleSO.UpdateBehavior();
    }

    public void OnExit()
    {


        idleSO.ExitBehavior();
    }
}
