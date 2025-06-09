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
        // �� ����
        Enemy targetEnemy = ROGUtility.GetClosestEnemy(player, player.characterRegistry.Enemies);

        if (targetEnemy == null) return;

        // �÷��̾� ȸ��
        Vector3 playerDir = targetEnemy.transform.position - player.transform.position;
        playerDir.y = 0;
        Quaternion dirQuat = Quaternion.LookRotation(playerDir);
        Quaternion nextQuat = Quaternion.Slerp(player.transform.rotation, dirQuat, player.playerStats.Movement.rotateSpeed * Time.deltaTime);
        player.transform.rotation = nextQuat;

        // ���� ��Ÿ�� ���
        if (Time.time < lastAttackTime + playerStats.Attack.cooldown * playerStats.Attack.cooldownRate) return;



        // ��Ÿ����?
        //float dist = Vector3.Distance(transform.position, enemy.transform.position);
        //if (dist > stats.Attack.range || !CanAttack()) return;



        // ����ü ���� ���
        Vector3 projectileSpawnPos = playerTransform.TransformPoint(playerResourceSO.PlayerModel.MuzzleOffset);
        Vector3 projectileDir = targetEnemy.transform.position - projectileSpawnPos;
        projectileDir.y = 0;
        Quaternion projectileRot = Quaternion.LookRotation(projectileDir.normalized);

        // ����ü ����, �ʱ�ȭ (����ü�� �ɼ� �������������� ���� �Լ��� ������ҵ�)
        GameObject projectileObj = Instantiate(playerResourceSO.ProjectileData.Prefab, projectileSpawnPos, projectileRot);
        var projectile = projectileObj.GetComponent<PlayerProjectile>();
        projectile.Initilize(playerResourceSO, playerStats.Projectile);

        // ���� ��Ÿ�� ����
        lastAttackTime = Time.time;
    }

    public override void ExitBehavior()
    {
        
    }
}
