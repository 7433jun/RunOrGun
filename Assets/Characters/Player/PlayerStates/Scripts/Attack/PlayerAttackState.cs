using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerAttackState : IROGState
{
    private Player player;
    private PlayerAttackBehaviorSO attackSO;
    private PlayerResourceSO resourceSO;
    private Transform playerTransform;
    private CharacterController playerController;
    private PlayerStats stats;
    private Enemy targetEnemy;

    private float lastAttackTime;

    public PlayerAttackState(Player player)
    {
        this.player = player;
        attackSO = player.playerDefinition.AttackSO;
        resourceSO = player.playerDefinition.ResourceSO;
        playerTransform = player.transform;
        playerController = player.GetComponent<CharacterController>();
        stats = player.playerStats;
    }

    public void OnEnter()
    {
        //Debug.Log("Player Attack");
        var ctx = CreatePlayerAttackContext();
        attackSO.EnterBehavior(ctx);
    }

    public void OnUpdate()
    {
        if (player.MoveInput != Vector2.zero)
        {
            player.StateMachine.ChangeState(player.PlayerMoveState);
            return;
        }

        targetEnemy = ROGUtility.GetClosestEnemy(player, player.characterRegistry.Enemies);

        if (targetEnemy == null)
        {
            player.StateMachine.ChangeState(player.PlayerIdleState);
            return;
        }
        // DieState 넘길 조건 추가

        // 플레이어 회전
        Vector3 dir = targetEnemy.transform.position - player.transform.position;
        dir.y = 0;
        Quaternion dirQuat = Quaternion.LookRotation(dir);
        Quaternion nextQuat = Quaternion.Slerp(player.transform.rotation, dirQuat, player.playerStats.Movement.rotateSpeed * Time.deltaTime);
        player.transform.rotation = nextQuat;

        // 쿨타임 계산
        if (Time.time < lastAttackTime + stats.Attack.cooldown * stats.Attack.cooldownRate) return;

        // 공격
        var ctx = CreatePlayerAttackContext();
        attackSO.Attack(ctx);

        lastAttackTime = Time.time;
    }

    public void OnExit()
    {
        var ctx = CreatePlayerAttackContext();
        attackSO.ExitBehavior(ctx);
    }

    private PlayerAttackContext CreatePlayerAttackContext()
    {
        return new PlayerAttackContext(
            targetEnemy,
            playerTransform,
            playerController,
            stats,
            resourceSO
            );
    }
}
