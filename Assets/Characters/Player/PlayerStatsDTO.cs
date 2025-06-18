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

    public float criticalDamageRatioBase;
    public float criticalDamageRatioBonus;

    public bool isRangeInfinite;
    public float rangeBase;
    public float rangeBonus;

    public float knockBackBase;
    public float knockBackBonus;

    public int simultaneousAttack;
}

public class PlayerAmmoStatsDTO
{
    public int magazineSizeBase;
    public int magazineSizeBonus;

    public bool isAmmoInfinite;
    public float reloadTimeBase;
    public float reloadTimeBonus;
    public float reloadTimeRatioRaw;

    public float angleBase;
    public float angleBonus;
    public float angleRatioRaw;
}

public class PlayerProjectileStatsDTO
{
    public float sizeRatioRaw;

    public float speedBase;
    public float speedRatioRaw;

    public float lifeTimeBase;
    public float lifeTimeRatioRaw;

    public int bounceWall;
    public int bounceEnemy;
    public int pierceEnemy;
}