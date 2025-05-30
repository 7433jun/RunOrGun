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
            if (player.characterRegistry.Enemies.Count == 0)
            {
                player.StateMachine.ChangeState(player.PlayerIdleState);
            }
            else
            {
                player.StateMachine.ChangeState(player.PlayerAttackState);
            }

            return;
        }
        // DieState 넘길 조건 추가

        player.Movement.Move(player.MoveInput);
    }

    public void OnExit()
    {

    }
}
