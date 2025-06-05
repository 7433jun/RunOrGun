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
        // ���� ���� ������ ���¸ӽ� ��ü���� üũ
        // ���� ���� ���ǵ� ������ ������ �ʿ䰡 ��������?
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
        // DieState �ѱ� ���� �߰�




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
