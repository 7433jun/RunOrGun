using System.Collections;
using System.Collections.Generic;
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

    private Queue<GameObject> projectilePool = new Queue<GameObject>();

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
        Quaternion nextQuat = Quaternion.Slerp(player.transform.rotation, dirQuat, playerStats.Move.RotateSpeed * Time.deltaTime);
        player.transform.rotation = nextQuat;

        // 공격 쿨타임 계산
        if (Time.time < lastAttackTime + playerStats.Attack.Speed) return;

        // 사거리계산
        if (!playerStats.Attack.IsRangeInfinite)
        {
            float dist = Vector3.Distance(playerTransform.position, targetEnemy.transform.position);
            if (dist > playerStats.Attack.Range) return;
        }

        // 탄 소모
        if (!playerStats.Ammo.IsAmmoInfinite)
            if (!playerStats.Ammo.TryUseAmmo(1)) return;

        // 투사체 방향 계산
        Vector3 projectileSpawnPos = playerTransform.TransformPoint(muzzleOffset);
        Vector3 projectileDir = targetEnemy.transform.position - projectileSpawnPos;
        projectileDir.y = 0;
        Quaternion projectileRot = Quaternion.LookRotation(projectileDir.normalized);

        // 투사체 생성, 초기화, 풀링 (투사체에 옵션 붙을수도있으니 별개 함수로 빼줘야할듯)
        GameObject projectileObj;
        if (projectilePool.Count > 0)
        {
            projectileObj = projectilePool.Dequeue();
            projectileObj.transform.SetPositionAndRotation(projectileSpawnPos, projectileRot);
            projectileObj.SetActive(true);
        }
        else
        {
            projectileObj = Instantiate(projectilePrefab, projectileSpawnPos, projectileRot);
        }
        var projectile = projectileObj.GetComponent<PlayerProjectile>();
        projectile.Initilize(playerStats.Projectile, projectileDir, projectilePool);

        // 남은 탄환 없으면 장전시작
        if (playerStats.Ammo.AmmoCurrent == 0)
            StartCoroutine(Reload());

        // 공격 쿨타임 갱신
        lastAttackTime = Time.time;
    }

    public override void ExitBehavior()
    {

    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(playerStats.Ammo.ReloadTime);
        playerStats.Ammo.ReloadAmmo();
    }
}
