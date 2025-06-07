using UnityEngine;

public class PlayerSpawnState : IROGState
{
    private Player player;
    private PlayerSpawnBehaviorSO spawnSO;

    public PlayerSpawnState(Player player)
    {
        this.player = player;
        spawnSO = Object.Instantiate(player.playerDefinition.SpawnSO);
        spawnSO.InitBehavior(player);
    }

    public void OnEnter()
    {

        spawnSO.EnterBehavior();
    }

    public void OnUpdate()
    {
        player.StateMachine.ChangeState(player.PlayerIdleState);

        spawnSO.UpdateBehavior();
    }

    public void OnExit()
    {


        spawnSO.ExitBehavior();
    }
}
