using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }

    public CharacterRegistry characterRegistry;


    public PlayerDefinitionSO playerDefinition;
    public PlayerStats playerStats;

    public StateMachine StateMachine { get; private set; }
    public IROGState PlayerStartState { get; private set; }
    public IROGState PlayerIdleState { get; private set; }
    public IROGState PlayerMoveState { get; private set; }
    public IROGState PlayerAttackState { get; private set; }
    public IROGState PlayerDieState { get; private set; }

    public PlayerVisual Visual { get; private set; }
    public PlayerMovementHandler Movement { get; private set; }
    public PlayerCombatHandler Combat { get; private set; }

    void Awake()
    {
        Visual = GetComponent<PlayerVisual>();
        Movement = GetComponent<PlayerMovementHandler>();
        Combat = GetComponent<PlayerCombatHandler>();
    }

    void Start()
    {
        // 로비씬에서 받아와서 적용시키기 지금은 임시 적용
        playerStats = new();

        playerStats.Movement.moveSpeed = 1f;
        playerStats.Movement.rotateSpeed = 30f;
        playerStats.Movement.sizeRate = 1f;

        playerStats.Attack.power = 10f;
        playerStats.Attack.powerRate = 1f;
        playerStats.Attack.range = 100f;
        playerStats.Attack.rangeRate = 1f;
        playerStats.Attack.cooldown = 0.5f;
        playerStats.Attack.cooldownRate = 1f;
        playerStats.Attack.angle = 30f;
        playerStats.Attack.angleRate = 1f;
        playerStats.Attack.projectileCount = 1;

        playerStats.Magazine.size = 10;
        playerStats.Magazine.reloadCooldown = 3f;
        playerStats.Magazine.reloadCooldownRate = 1f;

        playerStats.Projectile.sizeRate = 1f;
        playerStats.Projectile.speed = 5f;
        playerStats.Projectile.speedRate = 1f;
        playerStats.Projectile.lifeTime = 3f;
        playerStats.Projectile.lifeTimeRate = 1f;
        playerStats.Projectile.bounceWall = 0;
        playerStats.Projectile.bounceEnemy = 0;
        playerStats.Projectile.pierceEnemy = 0;

        Visual.Initialize(playerDefinition.ResourceSO);
        Movement.Initialize(playerStats);
        Combat.Initialize(playerDefinition.ResourceSO, playerStats);

        StateMachine = new StateMachine();
        PlayerStartState = new PlayerSpawnState(this);
        PlayerIdleState = new PlayerIdleState(this);
        PlayerMoveState = new PlayerMoveState(this);
        PlayerAttackState = new PlayerAttackState(this);
        PlayerDieState = new PlayerDieState(this);
        StateMachine.ChangeState(PlayerStartState);
    }

    void Update()
    {
        StateMachine.Update();
    }

    public void Initialize(CharacterRegistry registry)
    {
        characterRegistry = registry;
    }

    void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
}
