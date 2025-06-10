using UnityEngine;

public class PlayerIdleState : IROGState
{
    private Player player;
    private PlayerStateMachine playerStateMachine;
    //private PlayerIdleBehaviorSO idleSO;
    private PlayerIdleBehavior idle;

    public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        idle = player.GetComponent<PlayerIdleBehavior>();
        idle.InitBehavior(player);
    }

    public void OnEnter()
    {
        //Debug.Log("Player Idle");

        idle.EnterBehavior();
    }

    public void OnUpdate()
    {
        if (player.MoveInput != Vector2.zero)
        {
            playerStateMachine.StateMachine.ChangeState(playerStateMachine.PlayerMoveState);
            return;
        }
        if (player.characterRegistry.Enemies.Count != 0)
        {
            playerStateMachine.StateMachine.ChangeState(playerStateMachine.PlayerAttackState);
            return;
        }
        // DieState 넘길 조건 추가

        idle.UpdateBehavior();
    }

    public void OnExit()
    {


        idle.ExitBehavior();
    }
}
