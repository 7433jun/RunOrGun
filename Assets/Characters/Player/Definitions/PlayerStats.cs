using System;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    public PlayerHealthStats Health = new();
    public PlayerAttackStats Attack = new();
    public PlayerMovementStats Movement = new();
    public PlayerProjectileStats Projectile = new();

    //public void ApplyData(PlayerStatusDTO dto)
    //{
    //
    //}
}

[Serializable]
public class PlayerHealthStats
{
    [SerializeField] private float healthCurrent;
    [SerializeField] private float healthBase;
    [SerializeField] private float healthBonus;
    [SerializeField] private float healthRatio;
    [SerializeField] private float healthMax;
    [SerializeField] private float healthHealRatio;
    [SerializeField] private float healthDamageRatio;


    public float HealthCurrent => healthCurrent;
    public float HealthMax => healthMax;

    public event Action<float> OnPlayerHealed;
    public event Action<float> OnPlayerDamaged;
    public event Action OnPlayerDie;
    public event Action OnMaxHealthChanged;

    public void CalHealthMax()
    {
        healthMax = (healthBase + healthBonus) * healthRatio;
        if (healthMax < 1.0f)
        {
            healthMax = 1.0f;
        }
        if (healthCurrent > healthMax)
        {
            healthCurrent = healthMax;
        }

        OnMaxHealthChanged?.Invoke();
    }

    public void InitHealth(float _healthBase)
    {
        healthBase = _healthBase;
        healthBonus = 0f;
        healthRatio = 1.0f;
        healthHealRatio = 1.0f;
        healthDamageRatio = 1.0f;

        CalHealthMax();
        healthCurrent = healthMax;
    }

    public void HealHealth(float amount)
    {
        float finalAmount = amount * healthHealRatio;
        healthCurrent += finalAmount;
        if (healthCurrent > healthMax)
        {
            healthCurrent = healthMax;
        }

        OnPlayerHealed?.Invoke(finalAmount);
    }

    public void DamageHealth(float amount)
    {
        float finalAmount = amount * healthDamageRatio;
        healthCurrent -= finalAmount;
        if (healthCurrent <= 0)
        {
            healthCurrent = 0f;

            OnPlayerDie?.Invoke();
            return;
        }

        OnPlayerDamaged?.Invoke(finalAmount);
    }

    public void IncreaseHealthBonus(float amount)
    {
        healthBonus += amount;
        CalHealthMax();
    }

    public void DecreaseHealthBonus(float amount)
    {
        healthBonus -= amount;
        if (healthBonus <= -healthBase)
        {
            healthBonus = -healthBase + 1.0f;
        }
        CalHealthMax();
    }

    public void IncreaseHealthRatio(float amount)
    {
        healthRatio += amount;
        CalHealthMax();
    }

    public void DecreaseHealthRatio(float amount)
    {
        healthRatio -= amount;
        if (healthRatio < 0.01f)
        {
            healthRatio = 0.01f;
        }
        CalHealthMax();
    }

    public void IncreaseHealthHealRatio(float amount)
    {
        healthHealRatio += amount;
    }

    public void DecreaseHealthHealRatio(float amount)
    {
        healthHealRatio -= amount;
        if (healthHealRatio < 0)
        {
            healthHealRatio = 0;
        }
    }

    public void IncreaseHealthDamageRatio(float amount)
    {
        healthDamageRatio += amount;
    }

    public void DecreaseHealthDamageRatio(float amount)
    {
        healthDamageRatio -= amount;
        if (healthDamageRatio < 0.01f)
        {
            healthDamageRatio = 0.01f;
        }
    }
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
public class PlayerMovementStats
{
    public float moveSpeedBase;
    public float moveSpeedBonus;
    public float moveSpeedRatio;
    public float moveSpeedCurrent => (moveSpeedBase + moveSpeedBonus) * moveSpeedRatio;

    public float rotateSpeed; // 아마도 고정값

    public float sizeRate; // 히트박스 관련
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