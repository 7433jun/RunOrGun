using System;
using UnityEngine;

[Serializable]
public class PlayerStats : MonoBehaviour
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

    public event Action<float> OnPlayerHealed;
    public event Action<float> OnPlayerDamaged;
    public event Action OnPlayerDie;
    public event Action OnMaxHealthChanged;

    private void InitHealth(PlayerHealthStatsDTO dto)
    {
        // ü�� �ʱ�ȭ
        // ü�� ������ ���� ������ dto�� 5��
        // �� �ܴ� ��� ����� ��
        healthBase = dto.healthBase;
        healthBonusRaw = dto.healthBonusRaw;
        healthRatioRaw = dto.healthRatioRaw;
        healthHealRatioRaw = dto.healthHealRatioRaw;
        healthDamageRatioRaw = dto.healthDamageRatioRaw;

        healthBonus = Mathf.Max(healthBonusRaw, -healthBase + 1f);
        healthRatio = Mathf.Max(healthRatioRaw, 0.01f);
        healthHealRatio = Mathf.Max(healthHealRatioRaw, 0f);
        healthDamageRatio = Mathf.Max(healthDamageRatioRaw, 0.01f);

        CalHealthMax();
        healthCurrent = healthMax;
    }

    private void CalHealthMax()
    {
        float healthMaxBefore = healthMax;

        healthMax = (healthBase + healthBonus) * healthRatio;
        if (healthMax < 1.0f)
        {
            healthMax = 1.0f;
        }
        if (healthCurrent > healthMax)
        {
            healthCurrent = healthMax;
        }

        if (healthMax != healthMaxBefore)
        {
            OnMaxHealthChanged?.Invoke();
        }
    }

    // ȸ��
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

    // ������
    public void DamageHealth(float amount)
    {
        float healthDamageRatioClamp = Mathf.Max(healthHealRatioRaw, 0f);
        float finalAmount = amount * healthDamageRatioClamp;
        healthCurrent -= finalAmount;
        if (healthCurrent <= 0)
        {
            healthCurrent = 0f;

            OnPlayerDie?.Invoke();
            return;
        }

        OnPlayerDamaged?.Invoke(finalAmount);
    }

    // ü�� �߰��� ����
    public void IncreaseHealthBonus(float amount)
    {
        healthBonusRaw += amount;
        healthBonus = Mathf.Max(healthBonusRaw, -healthBase + 1f);
        CalHealthMax();
    }

    // ü�� �߰��� ����
    public void DecreaseHealthBonus(float amount)
    {
        healthBonusRaw -= amount;
        healthBonus = Mathf.Max(healthBonusRaw, -healthBase + 1f);
        CalHealthMax();
    }

    // ü�� ���� ����
    public void IncreaseHealthRatio(float amount)
    {
        healthRatioRaw += amount;
        healthRatio = Mathf.Max(healthRatioRaw, 0.01f);
        CalHealthMax();
    }

    // ü�� ���� ����
    public void DecreaseHealthRatio(float amount)
    {
        healthRatioRaw -= amount;
        healthRatio = Mathf.Max(healthRatioRaw, 0.01f);
        CalHealthMax();
    }

    // ȸ�� ���� ����
    public void IncreaseHealthHealRatio(float amount)
    {
        healthHealRatioRaw += amount;
        healthHealRatio = Mathf.Max(healthHealRatioRaw, 0f);
    }

    // ȸ�� ���� ����
    public void DecreaseHealthHealRatio(float amount)
    {
        healthHealRatioRaw -= amount;
        healthHealRatio = Mathf.Max(healthHealRatioRaw, 0f);
    }

    // �޴� ������ ���� ����
    public void IncreaseHealthDamageRatio(float amount)
    {
        healthDamageRatioRaw += amount;
        healthDamageRatio = Mathf.Max(healthDamageRatioRaw, 0.01f);
    }

    // �޴� ������ ���� ����
    public void DecreaseHealthDamageRatio(float amount)
    {
        healthDamageRatioRaw -= amount;
        healthDamageRatio = Mathf.Max(healthDamageRatioRaw, 0.01f);
    }
}

[Serializable]
public class PlayerMoveStats
{
    public float moveSpeedBase;
    public float moveSpeedBonus;
    public float moveSpeedRatio;
    public float moveSpeedCurrent => (moveSpeedBase + moveSpeedBonus) * moveSpeedRatio;

    public float rotateSpeed; // �Ƹ��� ������

    public float sizeRate; // ��Ʈ�ڽ� ����
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

    public int simultaneousAttackCount; // ���ݴ� ����Ƚ��
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

    public float angleBase; // �߻� �ִ밢��
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