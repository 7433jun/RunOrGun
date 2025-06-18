using UnityEngine;

public class PlayerMoveState : IROGState
{
    private Player player;
    private PlayerStateMachine playerStateMachine;
    private PlayerMoveBehavior move;

    public PlayerMoveState(Player player, PlayerStateMachine playerStateMachine)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        move = player.GetComponent<PlayerMoveBehavior>();
        move.InitBehavior(player);
    }

    public void OnEnter()
    {
        //Debug.Log("Player Move");

        move.EnterBehavior();
    }

    public void OnUpdate()
    {
        // 상태 조건
        if (player.MoveInput == Vector2.zero)
        {
            if (player.characterRegistry.Enemies.Count == 0)
            {
                playerStateMachine.StateMachine.ChangeState(playerStateMachine.PlayerIdleState);
            }
            else
            {
                playerStateMachine.StateMachine.ChangeState(playerStateMachine.PlayerAttackState);
            }

            return;
        }
        if (player.Stats.Health.HealthCurrent <= 0)
        {
            playerStateMachine.StateMachine.ChangeState(playerStateMachine.PlayerDieState);
            return;
        }


        move.UpdateBehavior();
    }

    public void OnExit()
    {


        move.ExitBehavior();
    }
}
