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

    // 피격시 무적시간 필요할지도

    // 체력 비율 최소값
    private readonly float healthRatioClamp = 0.01f;
    // 최대 체력 최소값
    private readonly float healthMaxClamp = 1.0f;
    // 회복 비율 최소값
    private readonly float healthHealRatioClamp = 0f;
    // 데미지 비율 최소값
    private readonly float healthDamageRatioClamp = 0.01f;

    public float Current => healthCurrent;
    public float Base => healthBase;
    public float Bonus => healthBonus;
    public float Ratio => healthRatio;
    public float Max => healthMax;
    public float HealRatio => healthHealRatio;
    public float DamageRatio => healthDamageRatio;

    // 회복량 전달
    public event Action<float> OnPlayerHealed;
    // 피해량 전달
    public event Action<float> OnPlayerDamaged;
    public event Action OnPlayerDie;
    public event Action OnMaxHealthChanged;

    public void InitHealth(PlayerHealthStatsDTO dto)
    {
        // 체력 초기화
        // 체력 연산의 기초 값들은 dto의 5개
        // 그 외는 모두 연산된 값
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

    // 회복
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

    // 데미지
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

    // 체력 추가값 증가
    public void IncreaseHealthBonus(float amount)
    {
        HealthBonus += amount;
    }

    // 체력 추가값 감소
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

    // 체력 비율 증가
    public void IncreaseHealthRatio(float amount)
    {
        HealthRatioRaw += amount;
    }

    // 체력 비율 감소
    public void DecreaseHealthRatio(float amount)
    {
        HealthRatioRaw -= amount;
    }

    // 회복 비율 증가
    public void IncreaseHealthHealRatio(float amount)
    {
        healthHealRatioRaw += amount;
        healthHealRatio = Mathf.Max(healthHealRatioRaw, healthHealRatioClamp);
    }

    // 회복 비율 감소
    public void DecreaseHealthHealRatio(float amount)
    {
        healthHealRatioRaw -= amount;
        healthHealRatio = Mathf.Max(healthHealRatioRaw, healthHealRatioClamp);
    }

    // 받는 데미지 비율 증가
    public void IncreaseHealthDamageRatio(float amount)
    {
        healthDamageRatioRaw += amount;
        healthDamageRatio = Mathf.Max(healthDamageRatioRaw, healthDamageRatioClamp);
    }

    // 받는 데미지 비율 감소
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
    [SerializeField] private float sizeRatio; // 히트박스 관련

    //
    private readonly float moveSpeedRatioClamp = 0.1f;
    // 사이즈 비율 최소값
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