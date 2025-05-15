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
        Debug.Log("Player Idle");
    }

    public void OnUpdate()
    {
        if (player.MoveInput != Vector2.zero)
        {
            player.StateMachine.ChangeState(player.PlayerMoveState);
            return;
        }
        if (CharacterRegistry.Instance?.Enemies.Count != 0)
        {
            player.StateMachine.ChangeState(player.PlayerAttackState);
            return;
        }
    }

    public void OnExit()
    {

    }
}
