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
        // 상태 조건
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
        // DieState 넘길 조건 추가

        attackSO.Attack();
    }

    public void OnExit()
    {
        attackSO.ExitBehavior();
    }
}
