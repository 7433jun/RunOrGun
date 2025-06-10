using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerAttackState : IROGState
{
    private Player player;
    private PlayerStateMachine playerStateMachine;
    private PlayerAttackBehavior attack;

    public PlayerAttackState(Player player, PlayerStateMachine playerStateMachine)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        attack = player.GetComponent<PlayerAttackBehavior>();
        attack.InitBehavior(player);
    }

    public void OnEnter()
    {
        //Debug.Log("Player Attack");

        attack.EnterBehavior();
    }

    public void OnUpdate()
    {
        // 상태 조건
        if (player.MoveInput != Vector2.zero)
        {
            playerStateMachine.StateMachine.ChangeState(playerStateMachine.PlayerMoveState);
            return;
        }
        if (player.characterRegistry.Enemies.Count == 0)
        {
            playerStateMachine.StateMachine.ChangeState(playerStateMachine.PlayerIdleState);
            return;
        }
        // DieState 넘길 조건 추가

        attack.UpdateBehavior();
    }

    public void OnExit()
    {
        attack.ExitBehavior();
    }
}
