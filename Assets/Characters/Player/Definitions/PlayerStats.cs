using System;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    public PlayerHealthStats Health = new();
    public PlayerMoveStats Move = new();
    public PlayerAttackStats Attack = new();
    public PlayerAmmoStats Ammo = new();
    public PlayerProjectileStats Projectile = new();
}

[Serializable]
public class PlayerHealthStats
{
    public float healthCurrent;

    public float healthBase;
    public float healthBonusRaw;
    public float healthBonus;
    public float healthRatioRaw;
    public float healthRatio;
    public float healthMax;

    public float healthHealRatioRaw;
    public float healthHealRatio;

    public float healthDamageRatioRaw;
    public float healthDamageRatio;
}

[Serializable]
public class PlayerMoveStats
{
    public float moveSpeedBase;
    public float moveSpeedBonus;
    public float moveSpeedRatio;
    public float moveSpeedCurrent => (moveSpeedBase + moveSpeedBonus) * moveSpeedRatio;

    public float rotateSpeed; // 아마도 고정값

    public float sizeRate; // 히트박스 관련
}

[Serializable]
public class PlayerAttackStats
{
    public float powerBase;
    public float powerBonus;
    public float powerRatio;
    public float powerCurrent => (powerBase + powerBonus) * powerRatio;

    public float attackSpeedBase;
    public float attackSpeedBonus;
    public float attackSpeedRatio;
    public float attackSpeedCurrent => (attackSpeedBase + attackSpeedBonus) * attackSpeedRatio;

    public float criticalRateBase;
    public float criticalRateBonus;
    public float criticalRateCurrent => criticalRateBase + criticalRateBonus;

    public float criticalDamageRatioBase;
    public float criticalDamageRatioBonus;
    public float criticalDamageRatioCurrent => criticalDamageRatioBase + criticalDamageRatioBonus;

    public float rangeBase;
    public float rangeBonus;
    public float rangeRatio;
    public float rangeCurrent => (rangeBase + rangeBonus) * rangeRatio;
    public bool rangeInfinite;

    public float knockBackBase;
    public float knockBackBonus;
    public float knockBackRatio;

    public int simultaneousAttackCount; // 공격당 공격횟수
}

[Serializable]
public class PlayerAmmoStats
{
    public int ammoCurrent;
    public int magazineSizeBase;
    public int magazineSizeBonus;
    public float magazineSizeRatio;
    public int magazineSizeCurrent => Mathf.CeilToInt((magazineSizeBase + magazineSizeBonus) * magazineSizeRatio);

    public float reloadTimeBase;
    public float reloadTimeBonus;
    public float reloadTimeRatio;
    public float reloadTimeCurrent => (reloadTimeBase + reloadTimeBonus) * reloadTimeRatio;

    public float angleBase; // 발사 최대각도
    public float angleBonus;
    public float angleRatio;
    public float angleCurrent => (angleBase + angleBonus) * angleRatio;
}

[Serializable]
public class PlayerProjectileStats
{
    public float sizeRatio;

    public float speed;
    public float speedRatio;
    public float speedCurrent => speed * speedRatio;

    public float lifeTime;
    public float lifeTimeRatio;
    public float lifeTimeCurrent => lifeTime * lifeTimeRatio;

    public int bounceWall;
    public int bounceEnemy;
    public int pierceEnemy;
}