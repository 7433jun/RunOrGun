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
        Attack.InitAttack(dto.Attack);
        Ammo.InitAmmo(dto.Ammo);
        Projectile.InitProjectile(dto.Projectile);
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
    [SerializeField] private readonly float healthRatioClamp = 0.01f;
    // 최대 체력 최소값
    [SerializeField] private readonly float healthMaxClamp = 1.0f;
    // 회복 비율 최소값
    [SerializeField] private readonly float healthHealRatioClamp = 0f;
    // 데미지 비율 최소값
    [SerializeField] private readonly float healthDamageRatioClamp = 0.01f;

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
    [SerializeField] private readonly float moveSpeedRatioClamp = 0.1f;
    // 플레이어 사이즈 비율 최소값
    [SerializeField] private readonly float sizeRatioClamp = 0.1f;


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
    [SerializeField] private float powerBase;
    [SerializeField] private float powerBonus;
    [SerializeField] private float powerRatioRaw;
    [SerializeField] private float powerRatio;
    [SerializeField] private float power;

    [SerializeField] private readonly float powerRatioClamp = 0.01f; // 공격력 비율 하한값
    [SerializeField] private readonly float powerClamp = 1f; // 공격력 하한값

    public float PowerBase => powerBase;
    public float PowerBonus => powerBonus;
    public float PowerRatio => powerRatio;
    public float Power => power;

    // 공격당 시간 간격(공격 속도)
    [SerializeField] private float attackSpeedBase;
    [SerializeField] private float attackSpeedBonus;
    [SerializeField] private float attackSpeedRatioRaw;
    [SerializeField] private float attackSpeedRatio;
    [SerializeField] private float attackSpeed;

    [SerializeField] private readonly float attackSpeedRatioClamp = 0.01f; // 공격속도 비율 하한값
    [SerializeField] private readonly float attackSpeedClamp = 0.01f; // 공격속도 하한값

    public float SpeedBase => attackSpeedBase;
    public float SpeedBonus => attackSpeedBonus;
    public float SpeedRatio => attackSpeedRatio;
    public float Speed => attackSpeed;

    // 치명타 확률
    [SerializeField] private float criticalRateBase;
    [SerializeField] private float criticalRateBonus;
    [SerializeField] private float criticalRate;

    public float CriticalRateBase => criticalRateBase;
    public float CriticalRateBonus => criticalRateBonus;
    public float CriticalRate => criticalRate;

    // 치명타 배율
    [SerializeField] private float criticalDamageRatioBase;
    [SerializeField] private float criticalDamageRatioBonus;
    [SerializeField] private float criticalDamageRatio;

    [SerializeField] private readonly float criticalDamageRatioClamp = 1f; // 치명타 배율 하한값

    public float CriticalDamageRatioBase => criticalDamageRatioBase;
    public float CriticalDamageRatioBonus => criticalDamageRatioBonus;
    public float CriticalDamageRatio => criticalDamageRatio;

    // 사거리
    [SerializeField] private bool isRangeInfinite;
    [SerializeField] private float rangeBase;
    [SerializeField] private float rangeBonus;
    [SerializeField] private float range;

    [SerializeField] private readonly float rangeClamp = 10f; // 사거리 하한값

    public bool IsRangeInfinite => isRangeInfinite;
    public float RangeBase => rangeBase;
    public float RangeBonus => rangeBonus;
    public float Range => range;

    // 넉백 강도
    [SerializeField] private float knockBackBase;
    [SerializeField] private float knockBackBonus;
    [SerializeField] private float knockBack;

    [SerializeField] private readonly float knockBackClamp = 0f; // 넉백강도 하한값

    public float KnockBackBase => knockBackBase;
    public float KnockBackBonus => knockBackBonus;
    public float KnockBack => knockBack;


    [SerializeField] private int simultaneousAttack; // 공격당 공격횟수

    public int SimultaneousAttack => simultaneousAttack;

    public void InitAttack(PlayerAttackStatsDTO dto)
    {
        powerBase = dto.powerBase;
        powerBonus = dto.powerBonus;
        powerRatioRaw = dto.powerRatioRaw;
        powerRatio = Mathf.Max(powerRatioRaw, powerRatioClamp);
        power = Mathf.Max((powerBase + powerBonus) * powerRatio, powerClamp);

        attackSpeedBase = dto.attackSpeedBase;
        attackSpeedBonus = dto.attackSpeedBonus;
        attackSpeedRatioRaw = dto.attackSpeedRatioRaw;
        attackSpeedRatio = Mathf.Max(attackSpeedRatioRaw, attackSpeedRatioClamp);
        attackSpeed = Mathf.Max((attackSpeedBase + attackSpeedBonus) * attackSpeedRatio, attackSpeedClamp);

        criticalRateBase = dto.criticalRateBase;
        criticalRateBonus = dto.criticalRateBonus;
        criticalRate = criticalRateBase + criticalRateBonus;

        criticalDamageRatioBase = dto.criticalDamageRatioBase;
        criticalDamageRatioBonus = dto.criticalDamageRatioBonus;
        criticalDamageRatio = Mathf.Max(criticalDamageRatioBase + criticalDamageRatioBonus, criticalDamageRatioClamp);

        isRangeInfinite = dto.isRangeInfinite;
        rangeBase = dto.rangeBase;
        rangeBonus = dto.rangeBonus;
        range = Mathf.Max(rangeBase + rangeBonus, rangeClamp);

        knockBackBase = dto.knockBackBase;
        knockBackBonus = dto.knockBackBonus;
        knockBack = Mathf.Max(knockBackBase + knockBackBonus, knockBackClamp);

        simultaneousAttack = dto.simultaneousAttack;
    }
}

[Serializable]
public class PlayerAmmoStats
{
    // 탄창크기 및 남은탄환수
    [SerializeField] private int ammoCurrent;
    [SerializeField] private int magazineSize;
    [SerializeField] private int magazineSizeBase;
    [SerializeField] private int magazineSizeBonus;

    [SerializeField] private readonly int magazineSizeClamp = 1; // 탄창크기 하한값

    public int AmmoCurrent => ammoCurrent;
    public int MagazineSize => magazineSize;
    public int MagazineSizeBase => magazineSizeBase;
    public int MagazineSizeBonus => magazineSizeBonus;

    // 재장전 시간(장전속도)
    [SerializeField] private bool isAmmoInfinite;
    [SerializeField] private float reloadTime;
    [SerializeField] private float reloadTimeBase;
    [SerializeField] private float reloadTimeBonus;
    [SerializeField] private float reloadTimeRatioRaw;
    [SerializeField] private float reloadTimeRatio;

    [SerializeField] private readonly float reloadTimeClamp = 0.01f; // 장전속도 하한값
    [SerializeField] private readonly float reloadTimeRatioClamp = 0.01f; // 장전속도 비율 하한값

    public bool IsAmmoInfinite => isAmmoInfinite;
    public float ReloadTime => reloadTime;
    public float ReloadTimeBase => reloadTimeBase;
    public float ReloadTimeBonus => reloadTimeBonus;
    public float ReloadTimeRatio => reloadTimeRatio;

    // 사격 최대 각도
    [SerializeField] private float angle;
    [SerializeField] private float angleBase;
    [SerializeField] private float angleBonus;
    [SerializeField] private float angleRatioRaw;
    [SerializeField] private float angleRatio;

    [SerializeField] private readonly float angleClamp = 0f; // 사격 각도 하한값
    [SerializeField] private readonly float angleRatioClamp = 0.01f; // 사격 각도 비율 하한값

    public float Angle => angle;
    public float AngleBase => angleBase;
    public float AngleBonus => angleBonus;
    public float AngleRatio => angleRatio;

    public void InitAmmo(PlayerAmmoStatsDTO dto)
    {
        magazineSizeBase = dto.magazineSizeBase;
        magazineSizeBonus = dto.magazineSizeBonus;
        magazineSize = Mathf.Max(magazineSizeBase + magazineSizeBonus, magazineSizeClamp);
        ammoCurrent = magazineSize;

        isAmmoInfinite = dto.isAmmoInfinite;
        reloadTimeBase = dto.reloadTimeBase;
        reloadTimeBonus = dto.reloadTimeBonus;
        reloadTimeRatioRaw = dto.reloadTimeRatioRaw;
        reloadTimeRatio = Mathf.Max(reloadTimeRatioRaw, reloadTimeRatioClamp);
        reloadTime = Mathf.Max((reloadTimeBase + reloadTimeBonus) * reloadTimeRatio, reloadTimeClamp);

        angleBase = dto.angleBase;
        angleBonus = dto.angleBonus;
        angleRatioRaw = dto.angleRatioRaw;
        angleRatio = Mathf.Max(angleRatioRaw, angleRatioClamp);
        angle = Mathf.Max((angleBase + angleBonus) * angleRatio, angleClamp);
    }

    public bool TryUseAmmo(int amount)
    {
        if (amount <= 0) return false;

        if (ammoCurrent >= amount)
        {
            ammoCurrent -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ReloadAmmo()
    {
        ammoCurrent = magazineSize;
    }
}

[Serializable]
public class PlayerProjectileStats
{
    // 투사체 사이즈 비율
    [SerializeField] private float sizeRatioRaw;
    [SerializeField] private float sizeRatio;

    [SerializeField] private readonly float sizeRatioClmap = 0.1f; // 투사체 사이즈 비율 하한값

    public float SizeRatio => sizeRatio;

    // 투사체 속도
    [SerializeField] private float speed;
    [SerializeField] private float speedBase;
    [SerializeField] private float speedRatioRaw;
    [SerializeField] private float speedRatio;

    [SerializeField] private readonly float speedClamp = 1f; // 투사체 속도 하한값
    [SerializeField] private readonly float speedRatioClamp = 0.01f; // 투사체 속도 비율 하한값

    public float Speed => speed;
    public float SpeedBase => speedBase;
    public float SpeedRatio => speedRatio;

    // 투사체 지속시간
    [SerializeField] private float lifeTime;
    [SerializeField] private float lifeTimeBase;
    [SerializeField] private float lifeTimeRatioRaw;
    [SerializeField] private float lifeTimeRatio;

    [SerializeField] private readonly float lifeTimeClamp = 1f; // 투사체 지속시간 하한값
    [SerializeField] private readonly float lifeTimeRatioClamp = 0.01f; // 투사체 지속시간 비율 하한값

    public float LifeTime => lifeTime;
    public float LifeTimeBase => lifeTimeBase;
    public float LifeTimeRatio => lifeTimeRatio;

    // 투사체 옵션
    [SerializeField] public int bounceWall; // 벽 반사 횟수
    [SerializeField] public int bounceEnemy; // 적 반사 횟수
    [SerializeField] public int pierceEnemy; // 적 관통 횟수
    
    public void InitProjectile(PlayerProjectileStatsDTO dto)
    {
        sizeRatioRaw = dto.sizeRatioRaw;
        sizeRatio = Mathf.Max(sizeRatioRaw, sizeRatioClmap);

        speedBase = dto.speedBase;
        speedRatioRaw = dto.speedRatioRaw;
        speedRatio = Mathf.Max(speedRatioRaw, speedRatioClamp);
        speed = Mathf.Max(speedBase * speedRatio, speedClamp);

        lifeTimeBase = dto.lifeTimeBase;
        lifeTimeRatioRaw = dto.lifeTimeRatioRaw;
        lifeTimeRatio = Mathf.Max(lifeTimeRatioRaw, lifeTimeRatioClamp);
        lifeTime = Mathf.Max(lifeTimeBase * lifeTimeRatio, lifeTimeClamp);

        bounceWall = dto.bounceWall;
        bounceEnemy = dto.bounceEnemy;
        pierceEnemy = dto.pierceEnemy;
    }
}