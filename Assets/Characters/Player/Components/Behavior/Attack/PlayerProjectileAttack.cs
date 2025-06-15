using UnityEngine;

public class PlayerProjectileAttack : PlayerAttackBehavior
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Vector3 muzzleOffset;

    private Player player;
    private Transform playerTransform;
    private CharacterController playerController;
    private PlayerStats playerStats;

    private float lastAttackTime;

    public override void InitBehavior(Player player)
    {
        this.player = player;
        playerTransform = player.transform;
        playerController = player.GetComponent<CharacterController>();
        playerStats = player.Stats;
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
        Quaternion nextQuat = Quaternion.Slerp(player.transform.rotation, dirQuat, playerStats.Move.rotateSpeed * Time.deltaTime);
        player.transform.rotation = nextQuat;

        // 공격 쿨타임 계산
        if (Time.time < lastAttackTime + playerStats.Attack.attackSpeedCurrent) return;



        // 사거리계산?
        //float dist = Vector3.Distance(transform.position, enemy.transform.position);
        //if (dist > stats.Attack.range || !CanAttack()) return;



        // 투사체 방향 계산
        Vector3 projectileSpawnPos = playerTransform.TransformPoint(muzzleOffset);
        Vector3 projectileDir = targetEnemy.transform.position - projectileSpawnPos;
        projectileDir.y = 0;
        Quaternion projectileRot = Quaternion.LookRotation(projectileDir.normalized);

        // 투사체 생성, 초기화 (투사체에 옵션 붙을수도있으니 별개 함수로 빼줘야할듯)
        GameObject projectileObj = Instantiate(projectilePrefab, projectileSpawnPos, projectileRot);
        var projectile = projectileObj.GetComponent<PlayerProjectile>();
        projectile.Initilize(playerStats.Projectile);

        // 공격 쿨타임 갱신
        lastAttackTime = Time.time;
    }

    public override void ExitBehavior()
    {

    }
}
