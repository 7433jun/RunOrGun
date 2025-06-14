public class PlayerStatsDTO
{
    public PlayerHealthStatsDTO Health;
    public PlayerMoveStatsDTO Move;
    public PlayerAttackStatsDTO Attack;
    public PlayerAmmoStatsDTO Ammo;
    public PlayerProjectileStatsDTO Projectile;
}

public class PlayerHealthStatsDTO
{
    public float healthBase;
    public float healthBonusRaw;
    public float healthRatioRaw;
    public float healthHealRatioRaw;
    public float healthDamageRatioRaw;
}

public class PlayerMoveStatsDTO
{
    public float moveSpeed;
    public float rotateSpeed;
    public float sizeRate;
}

public class PlayerAttackStatsDTO
{
    public float power;
    public float powerRate;
    public float range;
    public float rangeRate;
    public float cooldown;
    public float cooldownRate;
    public float angle;
    public float angleRate;
    public int projectileCount;
}

public class PlayerAmmoStatsDTO
{
    public int size;
    public float reloadCooldown;
    public float reloadCooldownRate;
}

public class PlayerProjectileStatsDTO
{
    public float sizeRate;
    public float speed;
    public float speedRate;
    public float lifeTime;
    public float lifeTimeRate;
    public int bounceWall;
    public int bounceEnemy;
    public int pierceEnemy;
}