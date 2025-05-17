[System.Serializable]
public class PlayerStatsDTO
{
    public PlayerMovementStatsDTO Movement;
    public PlayerAttackStatsDTO Attack;
    public PlayerMagazineStatsDTO Magazine;
    public PlayerProjectileStatsDTO Projectile;
}

[System.Serializable]
public class PlayerMovementStatsDTO
{
    public float moveSpeed;
    public float rotateSpeed;
    public float sizeRate;
}

[System.Serializable]
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

[System.Serializable]
public class PlayerMagazineStatsDTO
{
    public int size;
    public float reloadCooldown;
    public float reloadCooldownRate;
}

[System.Serializable]
public class PlayerProjectileStatsDTO
{
    public float size;
    public float sizeRate;
    public float speed;
    public int bounceWall;
    public int bounceEnemy;
    public int pierceEnemy;
}