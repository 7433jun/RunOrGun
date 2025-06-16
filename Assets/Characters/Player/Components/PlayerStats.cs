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

    public void InitPlayerStats(PlayerStatsDTO dto)
    {
        Health.InitHealth(dto.Health);
        Move.InitMove(dto.Move);
    }
}

[Serializable]
public class PlayerHealthStats
{
    [SerializeField] private bool isDead;
    [SerializeField] private float healthCurrent;

    [SerializeField] private float healthBase;
    [SerializeField] private float healthBonus;
    [SerializeField] private float healthRatioRaw;
    [SerializeField] private float healthRatio;
    [SerializeField] private float healthMax;

    [SerializeField] private float healthHealRatioRaw;
    [SerializeField] private float healthHealRatio;

    [SerializeField] private float healthDamageRatioRaw;
    [SerializeField] private float healthDamageRatio;

    // �ǰݽ� �����ð� �ʿ�������

    // ü�� ���� �ּҰ�
    private readonly float healthRatioClamp = 0.01f;
    // �ִ� ü�� �ּҰ�
    private readonly float healthMaxClamp = 1.0f;
    // ȸ�� ���� �ּҰ�
    private readonly float healthHealRatioClamp = 0f;
    // ������ ���� �ּҰ�
    private readonly float healthDamageRatioClamp = 0.01f;

    public float Current => healthCurrent;
    public float Base => healthBase;
    public float Bonus => healthBonus;
    public float Ratio => healthRatio;
    public float Max => healthMax;
    public float HealRatio => healthHealRatio;
    public float DamageRatio => healthDamageRatio;

    // ȸ���� ����
    public event Action<float> OnPlayerHealed;
    // ���ط� ����
    public event Action<float> OnPlayerDamaged;
    public event Action OnPlayerDie;
    public event Action OnMaxHealthChanged;

    public void InitHealth(PlayerHealthStatsDTO dto)
    {
        // ü�� �ʱ�ȭ
        // ü�� ������ ���� ������ dto�� 5��
        // �� �ܴ� ��� ����� ��
        healthBase = dto.healthBase;
        healthBonus = dto.healthBonusRaw;
        healthRatioRaw = dto.healthRatioRaw;
        healthHealRatioRaw = dto.healthHealRatioRaw;
        healthDamageRatioRaw = dto.healthDamageRatioRaw;

        healthRatio = Mathf.Max(healthRatioRaw, healthRatioClamp);
        healthHealRatio = Mathf.Max(healthHealRatioRaw, healthHealRatioClamp);
        healthDamageRatio = Mathf.Max(healthDamageRatioRaw, healthDamageRatioClamp);

        CalHealthMax();
        healthCurrent = healthMax;
        isDead = false;
    }

    private void CalHealthMax()
    {
        float healthMaxBefore = healthMax;

        healthMax = (healthBase + healthBonus) * healthRatio;
        if (healthMax < healthMaxClamp)
        {
            healthMax = healthMaxClamp;
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
        float finalAmount = amount * healthDamageRatio;
        healthCurrent -= finalAmount;
        if (healthCurrent <= 0 && !isDead)
        {
            healthCurrent = 0f;

            OnPlayerDie?.Invoke();
            isDead = true;
            return;
        }

        OnPlayerDamaged?.Invoke(finalAmount);
    }

    private float HealthBonus
    {
        get => healthBonus;
        set
        {
            healthBonus = value;
            CalHealthMax();
        }
    }

    // ü�� �߰��� ����
    public void IncreaseHealthBonus(float amount)
    {
        HealthBonus += amount;
    }

    // ü�� �߰��� ����
    public void DecreaseHealthBonus(float amount)
    {
        HealthBonus -= amount;
    }

    private float HealthRatioRaw
    {
        get => healthRatioRaw;
        set
        {
            healthRatioRaw = value;
            healthRatio = Mathf.Max(healthRatioRaw, healthRatioClamp);
            CalHealthMax();
        }
    }

    // ü�� ���� ����
    public void IncreaseHealthRatio(float amount)
    {
        HealthRatioRaw += amount;
    }

    // ü�� ���� ����
    public void DecreaseHealthRatio(float amount)
    {
        HealthRatioRaw -= amount;
    }

    // ȸ�� ���� ����
    public void IncreaseHealthHealRatio(float amount)
    {
        healthHealRatioRaw += amount;
        healthHealRatio = Mathf.Max(healthHealRatioRaw, healthHealRatioClamp);
    }

    // ȸ�� ���� ����
    public void DecreaseHealthHealRatio(float amount)
    {
        healthHealRatioRaw -= amount;
        healthHealRatio = Mathf.Max(healthHealRatioRaw, healthHealRatioClamp);
    }

    // �޴� ������ ���� ����
    public void IncreaseHealthDamageRatio(float amount)
    {
        healthDamageRatioRaw += amount;
        healthDamageRatio = Mathf.Max(healthDamageRatioRaw, healthDamageRatioClamp);
    }

    // �޴� ������ ���� ����
    public void DecreaseHealthDamageRatio(float amount)
    {
        healthDamageRatioRaw -= amount;
        healthDamageRatio = Mathf.Max(healthDamageRatioRaw, healthDamageRatioClamp);
    }
}

[Serializable]
public class PlayerMoveStats
{
    [SerializeField] private float moveSpeedBase;
    [SerializeField] private float moveSpeedRatioRaw;
    [SerializeField] private float moveSpeedRatio;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float rotateSpeed;

    [SerializeField] private float sizeRatioRaw;
    [SerializeField] private float sizeRatio; // ��Ʈ�ڽ� ����

    //
    private readonly float moveSpeedRatioClamp = 0.1f;
    // ������ ���� �ּҰ�
    private readonly float sizeRatioClamp = 0.1f;


    public float MoveSpeedBase => moveSpeedBase;
    public float MoveSpeedRatio => moveSpeedRatio;
    public float MoveSpeed => moveSpeed;
    public float RotateSpeed => rotateSpeed;
    public float SizeRatio => sizeRatio;

    public event Action OnSizeChanged;

    public void InitMove(PlayerMoveStatsDTO dto)
    {
        moveSpeedBase = dto.moveSpeedBase;
        moveSpeedRatio = dto.moveSpeedRatio;
        rotateSpeed = dto.rotateSpeed;
        sizeRatioRaw = dto.sizeRatio;


    }

    public void IncreaseMoveSpeedRatio(float amount)
    {
        moveSpeedRatioRaw += amount;
        moveSpeedRatio = Mathf.Max(moveSpeedRatioRaw, moveSpeedRatioClamp);
        moveSpeed = moveSpeedBase * moveSpeedRatio;
    }

    public void DecreaseMoveSpeedRatio(float amount)
    {
        moveSpeedRatioRaw -= amount;
        moveSpeedRatio = Mathf.Max(moveSpeedRatioRaw, moveSpeedRatioClamp);
        moveSpeed = moveSpeedBase * moveSpeedRatio;
    }

    public void IncreaseSizeRatio(float amount)
    {

    }

    public void DecreaseSizeRatio(float amount)
    {

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