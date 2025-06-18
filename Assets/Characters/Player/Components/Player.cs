using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int playerID;
    public string playerName;

    public Vector2 MoveInput { get; private set; }

    public CharacterRegistry characterRegistry;
    public PlayEvent playEvent;

    public PlayerStats Stats { get; private set; }
    public PlayerVisual Visual { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }

    void Awake()
    {
        Stats = GetComponent<PlayerStats>();
        Visual = GetComponent<PlayerVisual>();
        StateMachine = GetComponent<PlayerStateMachine>();
    }

    void Start()
    {
        PlayerStatsDTO dto = new PlayerStatsDTO();
        dto.Health.healthBase = 100f;
        dto.Health.healthBonusRaw = 0f;
        dto.Health.healthRatioRaw = 1f;
        dto.Health.healthHealRatioRaw = 1f;
        dto.Health.healthDamageRatioRaw = 1f;

        dto.Move.moveSpeedBase = 1f;
        dto.Move.moveSpeedRatioRaw = 1f;
        dto.Move.rotateSpeed = 30f;
        dto.Move.sizeRatioRaw = 1f;

        dto.Attack.powerBase = 10f;
        dto.Attack.powerBonus = 0f;
        dto.Attack.powerRatioRaw = 1f;
        dto.Attack.attackSpeedBase = 0.5f;
        dto.Attack.attackSpeedBonus = 0f;
        dto.Attack.attackSpeedRatioRaw = 1f;
        dto.Attack.criticalRateBase = 0f;
        dto.Attack.criticalRateBonus = 0f;
        dto.Attack.criticalDamageRatioBase = 1.5f;
        dto.Attack.criticalDamageRatioBonus = 0f;
        dto.Attack.isRangeInfinite = false;
        dto.Attack.rangeBase = 100f;
        dto.Attack.rangeBonus = 0f;
        dto.Attack.knockBackBase = 1f;
        dto.Attack.knockBackBonus = 0f;
        dto.Attack.simultaneousAttack = 1;

        dto.Ammo.magazineSizeBase = 10;
        dto.Ammo.magazineSizeBonus = 0;
        dto.Ammo.isAmmoInfinite = false;
        dto.Ammo.reloadTimeBase = 3f;
        dto.Ammo.reloadTimeBonus = 0f;
        dto.Ammo.reloadTimeRatioRaw = 1f;
        dto.Ammo.angleBase = 20f;
        dto.Ammo.angleBonus = 0f;
        dto.Ammo.angleRatioRaw = 1f;

        dto.Projectile.sizeRatioRaw = 1f;
        dto.Projectile.speedBase = 5f;
        dto.Projectile.speedRatioRaw = 1f;
        dto.Projectile.lifeTimeBase = 3f;
        dto.Projectile.lifeTimeRatioRaw = 1f;
        dto.Projectile.bounceWall = 0;
        dto.Projectile.bounceEnemy = 0;
        dto.Projectile.pierceEnemy = 0;
        Stats.InitPlayerStats(dto);

        //Visual.Initialize(DefinitionSO.ResourceSO);
        // 모델 데이터뿐아니라 애니메이션, 사운드, 이펙트 이런것도 프리팹에 클래스 구성해서 설정해놔야됨

        StateMachine.Initialize(this);

        // movement
        GetComponent<CharacterController>().skinWidth = 0.001f;

    }

    public void Initialize(PlayContext playContext)
    {
        characterRegistry = playContext.CharacterRegistry;
        playEvent = playContext.playEvent;
    }

    void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
}
