using UnityEngine;

public class EnemyAttackState : IROGState
{
    private Enemy enemy;
    private EnemyAttackBehaviorSO attackSO;

    public EnemyAttackState(Enemy enemy)
    {
        this.enemy = enemy;
        attackSO = Object.Instantiate(enemy.DefinitionSO.AttackSO);
        attackSO.InitBehavior(enemy);
    }

    public void OnEnter()
    {


        attackSO.EnterBehavior();
    }

    public void OnUpdate()
    {
        if (enemy.characterRegistry.Player == null)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyIdleState);
        }

        attackSO.UpdateBehavior();
    }

    public void OnExit()
    {


        attackSO.ExitBehavior();
    }
}
