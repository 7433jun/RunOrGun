using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{
    //[SerializeField] private WeaponDefinition currentWeapon;

    private PlayerDefinition weaponDefinition;
    private PlayerStats stats;


    public void Initialize(PlayerDefinition weaponDefinition, PlayerStats stats)
    {
        this.weaponDefinition = weaponDefinition;
        this.stats = stats;
    }

    private float lastAttackTime;

    public bool CanAttack() => Time.time >= lastAttackTime + stats.Attack.cooldown * stats.Attack.cooldownRate;

    public void TryAttack(Enemy enemy)
    {
        if (enemy == null || !CanAttack()) return;

        Vector3 projectileSpawnPos = transform.TransformPoint(weaponDefinition.weaponModel.muzzleOffset);

        Vector3 direction = enemy.transform.position - projectileSpawnPos;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);

        GameObject projectileObj = Instantiate(weaponDefinition.projectileData.prefab, projectileSpawnPos, rotation);

        var projectile = projectileObj.GetComponent<PlayerProjectile>();
        projectile.Initilize(weaponDefinition, stats.Projectile);

        //float dist = Vector3.Distance(transform.position, enemy.transform.position);
        //if (dist > stats.Attack.range || !CanAttack()) return;

        //enemy.takeDamage(damage);
        lastAttackTime = Time.time;
    }
}
