using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{
    //[SerializeField] private WeaponDefinition currentWeapon;

    private WeaponDefinition weaponDefinition;
    private PlayerStats stats;


    public void Initialize(WeaponDefinition weaponDefinition, PlayerStats stats)
    {
        this.weaponDefinition = weaponDefinition;
        this.stats = stats;
    }

    private float lastAttackTime;

    public bool CanAttack() => Time.time >= lastAttackTime + stats.Attack.cooldown * stats.Attack.cooldownRate;

    public void TryAttack(Enemy enemy)
    {
        if (enemy == null || !CanAttack()) return;

        Vector3 projectileSpawnPos = transform.TransformPoint(weaponDefinition.weaponData.muzzleOffset);

        Vector3 direction = enemy.transform.position - projectileSpawnPos;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);

        GameObject projectileObj = Instantiate(weaponDefinition.projectilePrefab, projectileSpawnPos, rotation);

        var projectile = projectileObj.GetComponent<Projectile>();
        projectile.Initilize(weaponDefinition, stats.Projectile);

        //float dist = Vector3.Distance(transform.position, enemy.transform.position);
        //if (dist > stats.Attack.range || !CanAttack()) return;

        //enemy.takeDamage(damage);
        lastAttackTime = Time.time;
    }
}
