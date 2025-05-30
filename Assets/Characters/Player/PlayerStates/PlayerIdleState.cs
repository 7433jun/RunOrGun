using UnityEngine;

public class PlayerIdleState : IROGState
{
    private Player player;

    public PlayerIdleState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        //Debug.Log("Player Idle");
    }

    public void OnUpdate()
    {
        if (player.MoveInput != Vector2.zero)
        {
            player.StateMachine.ChangeState(player.PlayerMoveState);
            return;
        }
        if (player.characterRegistry.Enemies.Count != 0)
        {
            player.StateMachine.ChangeState(player.PlayerAttackState);
            return;
        }

        Debug.Log(player.characterRegistry.Enemies.Count);
        // DieState 넘길 조건 추가
    }

    public void OnExit()
    {

    }
}
