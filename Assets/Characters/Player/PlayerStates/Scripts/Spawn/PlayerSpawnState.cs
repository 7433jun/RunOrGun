using UnityEngine;

public class PlayerSpawnState : IROGState
{
    private Player player;

    public PlayerSpawnState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        player.characterRegistry.Register(player);

        player.StateMachine.ChangeState(player.PlayerIdleState);
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}
