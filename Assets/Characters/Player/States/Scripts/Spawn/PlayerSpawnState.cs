using UnityEngine;

public class PlayerSpawnState : IROGState
{
    private Player player;
    private PlayerStateMachine playerStateMachine;
    private PlayerSpawnBehavior spawn;

    public PlayerSpawnState(Player player, PlayerStateMachine playerStateMachine)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        spawn = player.GetComponent<PlayerSpawnBehavior>();
        spawn.InitBehavior(player);
    }

    public void OnEnter()
    {


        spawn.EnterBehavior();
        
    }

    public void OnUpdate()
    {
        playerStateMachine.StateMachine.ChangeState(playerStateMachine.PlayerIdleState);

        spawn.UpdateBehavior();
    }

    public void OnExit()
    {


        spawn.ExitBehavior();
    }
}
