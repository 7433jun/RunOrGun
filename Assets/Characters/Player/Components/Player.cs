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

        // �̰� �� �־�ߵ�

        // ���� �ʱ�ȭ �Լ� ��¼��

        // �κ������ �޾ƿͼ� �����Ű�� ������ �ӽ� ����
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
        // �� �����ͻӾƴ϶� �ִϸ��̼�, ����, ����Ʈ �̷��͵� �����տ� Ŭ���� �����ؼ� �����س��ߵ�

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
