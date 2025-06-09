using UnityEngine;

public class PlayerDieState : IROGState
{
    private Player player;
    private PlayerDieBehaviorSO dieSO;

    public PlayerDieState(Player player)
    {
        this.player = player;
        dieSO = Object.Instantiate(player.DefinitionSO.DieSO);
        dieSO.InitBehavior(player);
    }

    public void OnEnter()
    {


        dieSO.EnterBehavior();
    }

    public void OnUpdate()
    {


        dieSO.UpdateBehavior();
    }

    public void OnExit()
    {


        dieSO.ExitBehavior();
    }
}
