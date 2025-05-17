using System;

[Serializable]
public class PlayerStats
{
    public PlayerMovementStats Movement = new();
    public PlayerAttackStats Attack = new();
    public PlayerMagazineStats Magazine = new();
    public PlayerProjectileStats Projectile = new();

    [Serializable]
    public class PlayerMovementStats
    {
        public float moveSpeed;
        public float rotateSpeed;
        public float sizeRate; // 히트박스 관련
    }

    [Serializable]
    public class PlayerAttackStats
    {
        public float power;
        public float powerRate;
        public float range;
        public float rangeRate;
        public float cooldown;
        public float cooldownRate;
        public float angle; // 탄환 발사 최대각도
        public float angleRate;
        public int projectileCount; // 공격당 투사체 개수
    }

    [Serializable]
    public class PlayerMagazineStats
    {
        public int size;
        public float reloadCooldown;
        public float reloadCooldownRate;
    }

    [Serializable]
    public class PlayerProjectileStats
    {
        public float size;
        public float sizeRate;
        public float speed;
        public int bounceWall;
        public int bounceEnemy;
        public int pierceEnemy;
    }

    //public void ApplyData(PlayerStatusDTO dto)
    //{
    //
    //}
}