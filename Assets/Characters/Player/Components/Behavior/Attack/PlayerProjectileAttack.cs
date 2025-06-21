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
        // �� ����
        Enemy targetEnemy = ROGUtility.GetClosestEnemy(player, player.characterRegistry.Enemies);
        if (targetEnemy == null) return;

        // �÷��̾� ȸ��
        Vector3 playerDir = targetEnemy.transform.position - player.transform.position;
        playerDir.y = 0;
        Quaternion dirQuat = Quaternion.LookRotation(playerDir);
        Quaternion nextQuat = Quaternion.Slerp(player.transform.rotation, dirQuat, playerStats.Move.RotateSpeed * Time.deltaTime);
        player.transform.rotation = nextQuat;

        // ���� ��Ÿ�� ���
        if (Time.time < lastAttackTime + playerStats.Attack.Speed) return;

        // ��Ÿ����
        if (!playerStats.Attack.IsRangeInfinite)
        {
            float dist = Vector3.Distance(playerTransform.position, targetEnemy.transform.position);
            if (dist > playerStats.Attack.Range) return;
        }

        // ź �Ҹ�
        if (!playerStats.Ammo.IsAmmoInfinite)
            if (!playerStats.Ammo.TryUseAmmo(1)) return;

        // ����ü ���� ���
        Vector3 projectileSpawnPos = playerTransform.TransformPoint(muzzleOffset);
        Vector3 projectileDir = targetEnemy.transform.position - projectileSpawnPos;
        projectileDir.y = 0;
        Quaternion projectileRot = Quaternion.LookRotation(projectileDir.normalized);

        // ����ü ����, �ʱ�ȭ, Ǯ�� (����ü�� �ɼ� �������������� ���� �Լ��� ������ҵ�)
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

        // ���� źȯ ������ ��������
        if (playerStats.Ammo.AmmoCurrent == 0)
            StartCoroutine(Reload());

        // ���� ��Ÿ�� ����
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
