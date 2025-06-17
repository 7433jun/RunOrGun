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

    [SerializeField] private float healedRatioRaw;
    [SerializeField] private float healedRatio;

    [SerializeField] private float damagedRatioRaw;
    [SerializeField] private float damagedRatio;

    // 피격시 무적시간 필요할지도

    // 체력 비율 최소값
    private readonly float healthRatioClamp = 0.01f;
    // 최대 체력 최소값
    private readonly float healthMaxClamp = 1.0f;
    // 회복 비율 최소값
    private readonly float healthHealRatioClamp = 0f;
    // 데미지 비율 최소값
    private readonly float healthDamageRatioClamp = 0.01f;

    public float HealthCurrent => healthCurrent;
    public float HealthBase => healthBase;
    public float HealthBonus => healthBonus;
    public float HealthRatio => healthRatio;
    public float HealthMax => healthMax;
    public float HealRatio => healedRatio;
    public float DamagedRatio => DamagedRatio;

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
        healedRatioRaw = dto.healthHealRatioRaw;
        damagedRatioRaw = dto.healthDamageRatioRaw;

        healthRatio = Mathf.Max(healthRatioRaw, healthRatioClamp);
        healedRatio = Mathf.Max(healedRatioRaw, healthHealRatioClamp);
        damagedRatio = Mathf.Max(damagedRatioRaw, healthDamageRatioClamp);

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
        float finalAmount = amount * healedRatio;
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
        float finalAmount = amount * DamagedRatio;
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

    // 체력 추가값 증가
    public void IncreaseHealthBonus(float amount)
    {
        healthBonus += amount;
        CalHealthMax();
    }

    // 체력 추가값 감소
    public void DecreaseHealthBonus(float amount)
    {
        healthBonus -= amount;
        CalHealthMax();
    }

    // 체력 비율 증가
    public void IncreaseHealthRatio(float amount)
    {
        healthRatioRaw += amount;
        healthRatio = Mathf.Max(healthRatioRaw, healthRatioClamp);
        CalHealthMax();
    }

    // 체력 비율 감소
    public void DecreaseHealthRatio(float amount)
    {
        healthRatioRaw -= amount;
        healthRatio = Mathf.Max(healthRatioRaw, healthRatioClamp);
        CalHealthMax();
    }

    // 회복 비율 증가
    public void IncreaseHealthHealRatio(float amount)
    {
        healedRatioRaw += amount;
        healedRatio = Mathf.Max(healedRatioRaw, healthHealRatioClamp);
    }

    // 회복 비율 감소
    public void DecreaseHealthHealRatio(float amount)
    {
        healedRatioRaw -= amount;
        healedRatio = Mathf.Max(healedRatioRaw, healthHealRatioClamp);
    }

    // 받는 데미지 비율 증가
    public void IncreaseHealthDamageRatio(float amount)
    {
        damagedRatioRaw += amount;
        damagedRatio = Mathf.Max(damagedRatioRaw, healthDamageRatioClamp);
    }

    // 받는 데미지 비율 감소
    public void DecreaseHealthDamageRatio(float amount)
    {
        damagedRatioRaw -= amount;
        damagedRatio = Mathf.Max(damagedRatioRaw, healthDamageRatioClamp);
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

    // 이동속도 비율 최소값
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
        moveSpeedRatioRaw = dto.moveSpeedRatioRaw;
        rotateSpeed = dto.rotateSpeed;
        sizeRatioRaw = dto.sizeRatioRaw;

        moveSpeedRatio = Mathf.Max(moveSpeedRatioRaw, moveSpeedRatioClamp);
        sizeRatio = Mathf.Max(sizeRatioRaw, sizeRatioClamp);

        moveSpeed = moveSpeedBase * moveSpeedRatio;
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
        sizeRatioRaw += amount;
        sizeRatio = Mathf.Max(sizeRatioRaw, sizeRatioClamp);

        OnSizeChanged?.Invoke();
    }

    public void DecreaseSizeRatio(float amount)
    {
        sizeRatioRaw -= amount;
        sizeRatio = Mathf.Max(sizeRatioRaw, sizeRatioClamp);

        OnSizeChanged?.Invoke();
    }
}

[Serializable]
public class PlayerAttackStats
{
    // 공격력
    private float powerBase;
    private float powerBonus;
    private float powerRatioRaw;
    private float powerRatio;
    private float power;

    private readonly float powerRatioClamp = 0.01f; // 공격력 비율 하한값
    private readonly float powerClmap = 1f; // 공격력 하한값

    public float PowerBase => powerBase;
    public float PowerBonus => powerBonus;
    public float PowerRatio => powerRatio;
    public float Power => power;

    // 공격속도
    private float attackSpeedBase;
    private float attackSpeedBonus;
    private float attackSpeedRatioRaw;
    private float attackSpeedRatio;
    private float attackSpeed;

    private readonly float attackSpeedRatioClamp = 0.01f; // 공격속도 비율 하한값
    private readonly float attackSpeedClamp = 0.01f; // 공격속도 하한값

    public float SpeedBase => attackSpeedBase;
    public float SpeedBonus => attackSpeedBonus;
    public float SpeedRatio => attackSpeedRatio;
    public float Speed => attackSpeed;

    // 치명타 확률
    private float criticalRateBase;
    private float criticalRateBonus;
    private float criticalRate;

    public float CriticalRateBase => criticalRateBase;
    public float CriticalRateBonus => criticalRateBonus;
    public float CriticalRate => criticalRate;

    // 치명타 배율
    private float criticalDamageRatioBase;
    private float criticalDamageRatioBonus;
    private float criticalDamageRatio;

    private readonly float criticalDamageRatioClmap = 1f; // 치명타 배율 하한값

    public float CriticalDamageRatioBase => criticalDamageRatioBase;
    public float CriticalDamageRatioBonus => criticalDamageRatioBonus;
    public float CriticalDamageRatio => criticalDamageRatio;

    // 사거리
    public float rangeBase;
    public float rangeBonus;
    public float rangeRatio;
    public float range => (rangeBase + rangeBonus) * rangeRatio;
    public bool rangeInfinite;

    // 넉백 강도
    public float knockBackBase;
    public float knockBackBonus;
    public float knockBackRatio;

    public int simultaneousAttackCount; // 공격당 공격횟수

    public void InitAttack(PlayerAttackStatsDTO dto)
    {
        powerBase = dto.powerBase;
        powerBonus = dto.powerBonus;
        powerRatioRaw = dto.powerRatioRaw;
        powerRatio = Mathf.Max(powerRatioRaw, powerRatioClamp);
        power = Mathf.Max((powerBase + powerBonus) * powerRatio, powerClmap);

        attackSpeedBase = dto.attackSpeedBase;
        attackSpeedBonus = dto.attackSpeedBonus;
        attackSpeedRatioRaw = dto.attackSpeedRatioRaw;
        attackSpeedRatio = Mathf.Max(attackSpeedRatioRaw, attackSpeedRatioClamp);
        attackSpeed = Mathf.Max((attackSpeedBase + attackSpeedBonus) * attackSpeedRatio, attackSpeedClamp);

        criticalRateBase = dto.criticalRateBase;
        criticalRateBonus = dto.criticalRateBonus;
        criticalRate = criticalRateBase + criticalRateBonus;
    }
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

    public void InitAmmo(PlayerAmmoStatsDTO dto)
    {

    }
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
    
    public void InitProjectile(PlayerProjectileStatsDTO dto)
    {

    }
}