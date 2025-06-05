using UnityEngine;

public class PlayerMoveState : IROGState
{
    private Player player;
    private PlayerMoveBehaviorSO moveSO;
    private Transform playerTransform;
    private CharacterController playerController;
    private PlayerMovementStats movementStats;

    public PlayerMoveState(Player player)
    {
        this.player = player;
        moveSO = player.playerDefinition.MoveSO;
        playerTransform = player.transform;
        playerController = player.GetComponent<CharacterController>();
        movementStats = player.playerStats.Movement;
    }

    public void OnEnter()
    {
        //Debug.Log("Player Move");
        var ctx = CreatePlayerMoveContext();
        moveSO.EnterBehavior(ctx);
    }

    public void OnUpdate()
    {
        // 상태 전이 조건은 상태머신 자체에서 체크
        // 상태 전이 조건도 개별로 구성할 필요가 있을지도?
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




        //player.Movement.Move(player.MoveInput);
        var ctx = CreatePlayerMoveContext();
        moveSO.Move(ctx);
    }

    public void OnExit()
    {
        var ctx = CreatePlayerMoveContext();
        moveSO.ExitBehavior(ctx);
    }

    private PlayerMoveContext CreatePlayerMoveContext()
    {
        return new PlayerMoveContext(
            player.MoveInput,
            playerTransform,
            playerController,
            movementStats
            );
    }
}
