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
        public float sizeRate; // ��Ʈ�ڽ� ����
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
        public float angle; // źȯ �߻� �ִ밢��
        public float angleRate;
        public int projectileCount; // ���ݴ� ����ü ����
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