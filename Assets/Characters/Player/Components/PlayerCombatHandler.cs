using Mono.Cecil;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{
    //[SerializeField] private WeaponDefinition currentWeapon;

    private PlayerResourceSO resource;
    private PlayerStats stats;


    public void Initialize(PlayerResourceSO resource, PlayerStats stats)
    {
        this.resource = resource;
        this.stats = stats;
    }

    private float lastAttackTime;

    public bool CanAttack() => Time.time >= lastAttackTime + stats.Attack.cooldown * stats.Attack.cooldownRate;

    public void TryAttack(Enemy enemy)
    {
        if (enemy == null || !CanAttack()) return;

        // ���ʹ� ��ġ�� ������� ���
        Vector3 projectileSpawnPos = transform.TransformPoint(resource.PlayerModel.MuzzleOffset);
        Vector3 direction = enemy.transform.position - projectileSpawnPos;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);

        // ����ü ����, �ʱ�ȭ (����ü�� �ɼ� �������������� ���� �Լ��� ������ҵ�)
        GameObject projectileObj = Instantiate(resource.ProjectileData.Prefab, projectileSpawnPos, rotation);
        var projectile = projectileObj.GetComponent<PlayerProjectile>();
        projectile.Initilize(resource, stats.Projectile);

        //float dist = Vector3.Distance(transform.position, enemy.transform.position);
        //if (dist > stats.Attack.range || !CanAttack()) return;

        //enemy.takeDamage(damage);
        lastAttackTime = Time.time;
    }
}
