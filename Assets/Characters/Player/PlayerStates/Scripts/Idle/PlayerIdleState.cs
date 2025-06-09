using UnityEngine;

public class PlayerIdleState : IROGState
{
    private Player player;
    private PlayerIdleBehaviorSO idleSO;

    public PlayerIdleState(Player player)
    {
        this.player = player;
        idleSO = Object.Instantiate(player.DefinitionSO.IdleSO);
        idleSO.InitBehavior(player);
    }

    public void OnEnter()
    {
        //Debug.Log("Player Idle");

        idleSO.EnterBehavior();
    }

    public void OnUpdate()
    {
        if (player.MoveInput != Vector2.zero)
        {
            player.StateMachine.ChangeState(player.PlayerMoveState);
            return;
        }
        if (player.characterRegistry.Enemies.Count != 0)
        {
            player.StateMachine.ChangeState(player.PlayerAttackState);
            return;
        }
        // DieState 넘길 조건 추가

        idleSO.UpdateBehavior();
    }

    public void OnExit()
    {


        idleSO.ExitBehavior();
    }
}
