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
        Debug.Log("IDLE");
    }

    public void OnUpdate()
    {
        if (player.MoveInput != Vector2.zero)
            player.StateMachine.ChangeState(player.PlayerMoveState);
    }

    public void OnExit()
    {

    }
}
