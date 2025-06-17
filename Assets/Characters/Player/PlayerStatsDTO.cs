public class PlayerStatsDTO
{
    public PlayerHealthStatsDTO Health = new();
    public PlayerMoveStatsDTO Move = new();
    public PlayerAttackStatsDTO Attack = new();
    public PlayerAmmoStatsDTO Ammo = new();
    public PlayerProjectileStatsDTO Projectile = new();
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
    public float moveSpeedBase;
    public float moveSpeedRatioRaw;
    public float rotateSpeed;
    public float sizeRatioRaw;
}

public class PlayerAttackStatsDTO
{
    public float powerBase;
    public float powerBonus;
    public float powerRatioRaw;

    public float attackSpeedBase;
    public float attackSpeedBonus;
    public float attackSpeedRatioRaw;

    public float criticalRateBase;
    public float criticalRateBonus;

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