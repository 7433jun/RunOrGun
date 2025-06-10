using UnityEngine;

public class PlayerDieState : IROGState
{
    private Player player;
    private PlayerStateMachine playerStateMachine;
    private PlayerDieBehavior die;

    public PlayerDieState(Player player, PlayerStateMachine playerStateMachine)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        die = player.GetComponent<PlayerDieBehavior>();
        die.InitBehavior(player);
    }

    public void OnEnter()
    {


        die.EnterBehavior();
    }

    public void OnUpdate()
    {


        die.UpdateBehavior();
    }

    public void OnExit()
    {


        die.ExitBehavior();
    }
}
