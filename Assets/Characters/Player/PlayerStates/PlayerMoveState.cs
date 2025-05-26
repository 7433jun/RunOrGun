using UnityEngine;

public class PlayerMoveState : IROGState
{
    private Player player;

    public PlayerMoveState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        Debug.Log("Player Move");
    }

    public void OnUpdate()
    {
        if (player.MoveInput == Vector2.zero)
        {
            if (CharacterRegistry.Instance.Enemies.Count == 0)
            {
                player.StateMachine.ChangeState(player.PlayerIdleState);
            }
            else
            {
                player.StateMachine.ChangeState(player.PlayerAttackState);
            }

            return;
        }

        player.Movement.Move(player.MoveInput);
    }

    public void OnExit()
    {

    }
}
