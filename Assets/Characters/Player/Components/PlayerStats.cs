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

    // �ǰݽ� �����ð� �ʿ�������

    // ü�� ���� �ּҰ�
    [SerializeField] private readonly float healthRatioClamp = 0.01f;
    // �ִ� ü�� �ּҰ�
    [SerializeField] private readonly float healthMaxClamp = 1.0f;
    // ȸ�� ���� �ּҰ�
    [SerializeField] private readonly float healthHealRatioClamp = 0f;
    // ������ ���� �ּҰ�
    [SerializeField] private readonly float healthDamageRatioClamp = 0.01f;

    public float HealthCurrent => healthCurrent;
    public float HealthBase => healthBase;
    public float HealthBonus => healthBonus;
    public float HealthRatio => healthRatio;
    public float HealthMax => healthMax;
    public float HealRatio => healedRatio;
    public float DamagedRatio => DamagedRatio;

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

    // ȸ��
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

    // ������
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

    // ü�� �߰��� ����
    public void IncreaseHealthBonus(float amount)
    {
        healthBonus += amount;
        CalHealthMax();
    }

    // ü�� �߰��� ����
    public void DecreaseHealthBonus(float amount)
    {
        healthBonus -= amount;
        CalHealthMax();
    }

    // ü�� ���� ����
    public void IncreaseHealthRatio(float amount)
    {
        healthRatioRaw += amount;
        healthRatio = Mathf.Max(healthRatioRaw, healthRatioClamp);
        CalHealthMax();
    }

    // ü�� ���� ����
    public void DecreaseHealthRatio(float amount)
    {
        healthRatioRaw -= amount;
        healthRatio = Mathf.Max(healthRatioRaw, healthRatioClamp);
        CalHealthMax();
    }

    // ȸ�� ���� ����
    public void IncreaseHealthHealRatio(float amount)
    {
        healedRatioRaw += amount;
        healedRatio = Mathf.Max(healedRatioRaw, healthHealRatioClamp);
    }

    // ȸ�� ���� ����
    public void DecreaseHealthHealRatio(float amount)
    {
        healedRatioRaw -= amount;
        healedRatio = Mathf.Max(healedRatioRaw, healthHealRatioClamp);
    }

    // �޴� ������ ���� ����
    public void IncreaseHealthDamageRatio(float amount)
    {
        damagedRatioRaw += amount;
        damagedRatio = Mathf.Max(damagedRatioRaw, healthDamageRatioClamp);
    }

    // �޴� ������ ���� ����
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
    [SerializeField] private float sizeRatio; // ��Ʈ�ڽ� ����

    // �̵��ӵ� ���� �ּҰ�
    [SerializeField] private readonly float moveSpeedRatioClamp = 0.1f;
    // �÷��̾� ������ ���� �ּҰ�
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
    // ���ݷ�
    [SerializeField] private float powerBase;
    [SerializeField] private float powerBonus;
    [SerializeField] private float powerRatioRaw;
    [SerializeField] private float powerRatio;
    [SerializeField] private float power;

    [SerializeField] private readonly float powerRatioClamp = 0.01f; // ���ݷ� ���� ���Ѱ�
    [SerializeField] private readonly float powerClamp = 1f; // ���ݷ� ���Ѱ�

    public float PowerBase => powerBase;
    public float PowerBonus => powerBonus;
    public float PowerRatio => powerRatio;
    public float Power => power;

    // ���ݴ� �ð� ����(���� �ӵ�)
    [SerializeField] private float attackSpeedBase;
    [SerializeField] private float attackSpeedBonus;
    [SerializeField] private float attackSpeedRatioRaw;
    [SerializeField] private float attackSpeedRatio;
    [SerializeField] private float attackSpeed;

    [SerializeField] private readonly float attackSpeedRatioClamp = 0.01f; // ���ݼӵ� ���� ���Ѱ�
    [SerializeField] private readonly float attackSpeedClamp = 0.01f; // ���ݼӵ� ���Ѱ�

    public float SpeedBase => attackSpeedBase;
    public float SpeedBonus => attackSpeedBonus;
    public float SpeedRatio => attackSpeedRatio;
    public float Speed => attackSpeed;

    // ġ��Ÿ Ȯ��
    [SerializeField] private float criticalRateBase;
    [SerializeField] private float criticalRateBonus;
    [SerializeField] private float criticalRate;

    public float CriticalRateBase => criticalRateBase;
    public float CriticalRateBonus => criticalRateBonus;
    public float CriticalRate => criticalRate;

    // ġ��Ÿ ����
    [SerializeField] private float criticalDamageRatioBase;
    [SerializeField] private float criticalDamageRatioBonus;
    [SerializeField] private float criticalDamageRatio;

    [SerializeField] private readonly float criticalDamageRatioClamp = 1f; // ġ��Ÿ ���� ���Ѱ�

    public float CriticalDamageRatioBase => criticalDamageRatioBase;
    public float CriticalDamageRatioBonus => criticalDamageRatioBonus;
    public float CriticalDamageRatio => criticalDamageRatio;

    // ��Ÿ�
    [SerializeField] private bool isRangeInfinite;
    [SerializeField] private float rangeBase;
    [SerializeField] private float rangeBonus;
    [SerializeField] private float range;

    [SerializeField] private readonly float rangeClamp = 10f; // ��Ÿ� ���Ѱ�

    public bool IsRangeInfinite => isRangeInfinite;
    public float RangeBase => rangeBase;
    public float RangeBonus => rangeBonus;
    public float Range => range;

    // �˹� ����
    [SerializeField] private float knockBackBase;
    [SerializeField] private float knockBackBonus;
    [SerializeField] private float knockBack;

    [SerializeField] private readonly float knockBackClamp = 0f; // �˹鰭�� ���Ѱ�

    public float KnockBackBase => knockBackBase;
    public float KnockBackBonus => knockBackBonus;
    public float KnockBack => knockBack;


    [SerializeField] private int simultaneousAttack; // ���ݴ� ����Ƚ��

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
    // źâũ�� �� ����źȯ��
    [SerializeField] private int ammoCurrent;
    [SerializeField] private int magazineSize;
    [SerializeField] private int magazineSizeBase;
    [SerializeField] private int magazineSizeBonus;

    [SerializeField] private readonly int magazineSizeClamp = 1; // źâũ�� ���Ѱ�

    public int AmmoCurrent => ammoCurrent;
    public int MagazineSize => magazineSize;
    public int MagazineSizeBase => magazineSizeBase;
    public int MagazineSizeBonus => magazineSizeBonus;

    // ������ �ð�(�����ӵ�)
    [SerializeField] private bool isAmmoInfinite;
    [SerializeField] private float reloadTime;
    [SerializeField] private float reloadTimeBase;
    [SerializeField] private float reloadTimeBonus;
    [SerializeField] private float reloadTimeRatioRaw;
    [SerializeField] private float reloadTimeRatio;

    [SerializeField] private readonly float reloadTimeClamp = 0.01f; // �����ӵ� ���Ѱ�
    [SerializeField] private readonly float reloadTimeRatioClamp = 0.01f; // �����ӵ� ���� ���Ѱ�

    public bool IsAmmoInfinite => isAmmoInfinite;
    public float ReloadTime => reloadTime;
    public float ReloadTimeBase => reloadTimeBase;
    public float ReloadTimeBonus => reloadTimeBonus;
    public float ReloadTimeRatio => reloadTimeRatio;

    // ��� �ִ� ����
    [SerializeField] private float angle;
    [SerializeField] private float angleBase;
    [SerializeField] private float angleBonus;
    [SerializeField] private float angleRatioRaw;
    [SerializeField] private float angleRatio;

    [SerializeField] private readonly float angleClamp = 0f; // ��� ���� ���Ѱ�
    [SerializeField] private readonly float angleRatioClamp = 0.01f; // ��� ���� ���� ���Ѱ�

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
    // ����ü ������ ����
    [SerializeField] private float sizeRatioRaw;
    [SerializeField] private float sizeRatio;

    [SerializeField] private readonly float sizeRatioClmap = 0.1f; // ����ü ������ ���� ���Ѱ�

    public float SizeRatio => sizeRatio;

    // ����ü �ӵ�
    [SerializeField] private float speed;
    [SerializeField] private float speedBase;
    [SerializeField] private float speedRatioRaw;
    [SerializeField] private float speedRatio;

    [SerializeField] private readonly float speedClamp = 1f; // ����ü �ӵ� ���Ѱ�
    [SerializeField] private readonly float speedRatioClamp = 0.01f; // ����ü �ӵ� ���� ���Ѱ�

    public float Speed => speed;
    public float SpeedBase => speedBase;
    public float SpeedRatio => speedRatio;

    // ����ü ���ӽð�
    [SerializeField] private float lifeTime;
    [SerializeField] private float lifeTimeBase;
    [SerializeField] private float lifeTimeRatioRaw;
    [SerializeField] private float lifeTimeRatio;

    [SerializeField] private readonly float lifeTimeClamp = 1f; // ����ü ���ӽð� ���Ѱ�
    [SerializeField] private readonly float lifeTimeRatioClamp = 0.01f; // ����ü ���ӽð� ���� ���Ѱ�

    public float LifeTime => lifeTime;
    public float LifeTimeBase => lifeTimeBase;
    public float LifeTimeRatio => lifeTimeRatio;

    // ����ü �ɼ�
    [SerializeField] public int bounceWall; // �� �ݻ� Ƚ��
    [SerializeField] public int bounceEnemy; // �� �ݻ� Ƚ��
    [SerializeField] public int pierceEnemy; // �� ���� Ƚ��
    
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