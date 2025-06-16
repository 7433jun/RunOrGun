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
        dto.Move.moveSpeedRatio = 1f;
        dto.Move.rotateSpeed = 30f;
        dto.Move.sizeRatio = 1f;

        Stats.InitPlayerStats(dto);

        // 이거 값 넣어야됨

        // 스탯 초기화 함수 어쩌구

        // 로비씬에서 받아와서 적용시키기 지금은 임시 적용
        //playerStats.Attack.power = 10f;
        //playerStats.Attack.powerRate = 1f;
        //playerStats.Attack.range = 100f;
        //playerStats.Attack.rangeRate = 1f;
        //playerStats.Attack.cooldown = 0.5f;
        //playerStats.Attack.cooldownRate = 1f;
        //playerStats.Attack.angle = 30f;
        //playerStats.Attack.angleRatio = 1f;
        //playerStats.Attack.projectileCount = 1;
        //
        //playerStats.Magazine.size = 10;
        //playerStats.Magazine.reloadCooldown = 3f;
        //playerStats.Magazine.reloadCooldownRate = 1f;
        //
        //playerStats.Projectile.sizeRate = 1f;
        //playerStats.Projectile.speed = 5f;
        //playerStats.Projectile.speedRate = 1f;
        //playerStats.Projectile.lifeTime = 3f;
        //playerStats.Projectile.lifeTimeRate = 1f;
        //playerStats.Projectile.bounceWall = 0;
        //playerStats.Projectile.bounceEnemy = 0;
        //playerStats.Projectile.pierceEnemy = 0;

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
