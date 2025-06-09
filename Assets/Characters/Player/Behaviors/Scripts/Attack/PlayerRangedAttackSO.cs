using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProjectileAttackSO", menuName = "Scriptable Objects/Player/Behavior/PlayerProjectileAttackSO")]
public class PlayerRangedAttackSO : PlayerAttackBehaviorSO
{
    private Player player;
    private PlayerResourceSO playerResourceSO;
    private Transform playerTransform;
    private CharacterController playerController;
    private PlayerStats playerStats;

    private float lastAttackTime;

    public override void InitBehavior(Player player)
    {
        this.player = player;
        playerResourceSO = player.DefinitionSO.ResourceSO;
        playerTransform = player.transform;
        playerController = player.GetComponent<CharacterController>();
        playerStats = player.playerStats;
    }

    public override void EnterBehavior()
    {
        
    }

    public override void UpdateBehavior()
    {
        // 적 감지
        Enemy targetEnemy = ROGUtility.GetClosestEnemy(player, player.characterRegistry.Enemies);

        if (targetEnemy == null) return;

        // 플레이어 회전
        Vector3 playerDir = targetEnemy.transform.position - player.transform.position;
        playerDir.y = 0;
        Quaternion dirQuat = Quaternion.LookRotation(playerDir);
        Quaternion nextQuat = Quaternion.Slerp(player.transform.rotation, dirQuat, player.playerStats.Movement.rotateSpeed * Time.deltaTime);
        player.transform.rotation = nextQuat;

        // 공격 쿨타임 계산
        if (Time.time < lastAttackTime + playerStats.Attack.cooldown * playerStats.Attack.cooldownRate) return;



        // 사거리계산?
        //float dist = Vector3.Distance(transform.position, enemy.transform.position);
        //if (dist > stats.Attack.range || !CanAttack()) return;



        // 투사체 방향 계산
        Vector3 projectileSpawnPos = playerTransform.TransformPoint(playerResourceSO.PlayerModel.MuzzleOffset);
        Vector3 projectileDir = targetEnemy.transform.position - projectileSpawnPos;
        projectileDir.y = 0;
        Quaternion projectileRot = Quaternion.LookRotation(projectileDir.normalized);

        // 투사체 생성, 초기화 (투사체에 옵션 붙을수도있으니 별개 함수로 빼줘야할듯)
        GameObject projectileObj = Instantiate(playerResourceSO.ProjectileData.Prefab, projectileSpawnPos, projectileRot);
        var projectile = projectileObj.GetComponent<PlayerProjectile>();
        projectile.Initilize(playerResourceSO, playerStats.Projectile);

        // 공격 쿨타임 갱신
        lastAttackTime = Time.time;
    }

    public override void ExitBehavior()
    {
        
    }
}
