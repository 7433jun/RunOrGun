using System;
using UnityEngine;

public class PlayerHealthStatSystem
{
    private PlayerHealthStats health;

    public PlayerHealthStatSystem(PlayerHealthStats health, PlayerHealthStatsDTO dto)
    {
        this.health = health;
        InitHealth(dto);
    }

    public float HealthCurrent => health.healthCurrent;
    public float HealthMax => health.healthMax;

    public event Action<float> OnPlayerHealed;
    public event Action<float> OnPlayerDamaged;
    public event Action OnPlayerDie;
    public event Action OnMaxHealthChanged;

    private void InitHealth(PlayerHealthStatsDTO dto)
    {
        health.healthBase = dto.healthBase;
        health.healthBonusRaw = dto.healthBonusRaw;
        health.healthRatioRaw = dto.healthRatioRaw;
        health.healthHealRatioRaw = dto.healthHealRatioRaw;
        health.healthDamageRatioRaw = dto.healthDamageRatioRaw;

        health.healthBonus = Mathf.Max(health.healthBonusRaw, -health.healthBase + 1f);
        health.healthRatio = Mathf.Max(health.healthRatioRaw, 0.01f);
        health.healthHealRatio = Mathf.Max(health.healthHealRatioRaw, 0f);
        health.healthDamageRatio = Mathf.Max(health.healthDamageRatioRaw, 0.01f);

        CalHealthMax();
        health.healthCurrent = health.healthMax;
    }

    private void CalHealthMax()
    {
        health.healthMax = (health.healthBase + health.healthBonus) * health.healthRatio;
        if (health.healthMax < 1.0f)
        {
            health.healthMax = 1.0f;
        }
        if (health.healthCurrent > health.healthMax)
        {
            health.healthCurrent = health.healthMax;
        }

        OnMaxHealthChanged?.Invoke();
    }

    public void HealHealth(float amount)
    {
        float finalAmount = amount * health.healthHealRatio;
        health.healthCurrent += finalAmount;
        if (health.healthCurrent > health.healthMax)
        {
            health.healthCurrent = health.healthMax;
        }

        OnPlayerHealed?.Invoke(finalAmount);
    }

    public void DamageHealth(float amount)
    {
        float healthDamageRatioClamp = Mathf.Max(health.healthHealRatioRaw, 0f);
        float finalAmount = amount * healthDamageRatioClamp;
        health.healthCurrent -= finalAmount;
        if (health.healthCurrent <= 0)
        {
            health.healthCurrent = 0f;

            OnPlayerDie?.Invoke();
            return;
        }

        OnPlayerDamaged?.Invoke(finalAmount);
    }

    public void IncreaseHealthBonus(float amount)
    {
        health.healthBonusRaw += amount;
        health.healthBonus = Mathf.Max(health.healthBonusRaw, -health.healthBase + 1f);
        CalHealthMax();
    }

    public void DecreaseHealthBonus(float amount)
    {
        health.healthBonusRaw -= amount;
        health.healthBonus = Mathf.Max(health.healthBonusRaw, -health.healthBase + 1f);
        CalHealthMax();
    }

    public void IncreaseHealthRatio(float amount)
    {
        health.healthRatioRaw += amount;
        health.healthRatio = Mathf.Max(health.healthRatioRaw, 0.01f);
        CalHealthMax();
    }

    public void DecreaseHealthRatio(float amount)
    {
        health.healthRatioRaw -= amount;
        health.healthRatio = Mathf.Max(health.healthRatioRaw, 0.01f);
        CalHealthMax();
    }

    public void IncreaseHealthHealRatio(float amount)
    {
        health.healthHealRatioRaw += amount;
        //health.healthHealRatioClamp = Mathf.Max(health.healthHealRatioRaw, 0f);
    }

    public void DecreaseHealthHealRatio(float amount)
    {
        health.healthHealRatioRaw -= amount;
        health.healthHealRatio = Mathf.Max(health.healthHealRatioRaw, 0f);
    }

    public void IncreaseHealthDamageRatio(float amount)
    {
        health.healthDamageRatioRaw += amount;
        health.healthDamageRatio = Mathf.Max(health.healthDamageRatioRaw, 0.01f);
    }

    public void DecreaseHealthDamageRatio(float amount)
    {
        health.healthDamageRatioRaw -= amount;
        health.healthDamageRatio = Mathf.Max(health.healthDamageRatioRaw, 0.01f);
    }
}
