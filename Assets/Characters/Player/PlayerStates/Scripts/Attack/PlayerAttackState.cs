using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerAttackState : IROGState
{
    private Player player;
    private PlayerAttackBehaviorSO attackSO;
    private Enemy targetEnemy;

    public PlayerAttackState(Player player)
    {
        this.player = player;
        attackSO = Object.Instantiate(player.playerDefinition.AttackSO);
        attackSO.InitBehavior(player);
    }

    public void OnEnter()
    {
        //Debug.Log("Player Attack");

        attackSO.EnterBehavior();
    }

    public void OnUpdate()
    {
        // ���� ����
        if (player.MoveInput != Vector2.zero)
        {
            player.StateMachine.ChangeState(player.PlayerMoveState);
            return;
        }
        if (player.characterRegistry.Enemies.Count == 0)
        {
            player.StateMachine.ChangeState(player.PlayerIdleState);
            return;
        }
        // DieState �ѱ� ���� �߰�

        attackSO.Attack();
    }

    public void OnExit()
    {
        attackSO.ExitBehavior();
    }
}
