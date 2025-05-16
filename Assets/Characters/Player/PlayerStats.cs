using System;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    public MovementStats Movement = new();
    public AttackStats Attack = new();
    public MagazineStats Magazine = new();
    public ProjectileStats Projectile = new();

    [Serializable]
    public class MovementStats
    {
        public float moveSpeed;
        public float rotateSpeed;
        public float sizeRate; // ��Ʈ�ڽ� ����
    }

    [Serializable]
    public class AttackStats
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
    public class MagazineStats
    {
        public int size;
        public float reloadCooldown;
        public float reloadCooldownRate;
    }

    [Serializable]
    public class ProjectileStats
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