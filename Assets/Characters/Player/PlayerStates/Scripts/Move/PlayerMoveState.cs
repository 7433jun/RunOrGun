using UnityEngine;

public class PlayerMoveState : IROGState
{
    private Player player;
    private PlayerMoveBehaviorSO moveSO;

    public PlayerMoveState(Player player)
    {
        this.player = player;
        moveSO = Object.Instantiate(player.playerDefinition.MoveSO);
        moveSO.InitBehavior(player);
    }

    public void OnEnter()
    {
        //Debug.Log("Player Move");
        moveSO.EnterBehavior();
    }

    public void OnUpdate()
    {
        // 상태 조건
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


        moveSO.UpdateBehavior();
    }

    public void OnExit()
    {
        moveSO.ExitBehavior();
    }
}
