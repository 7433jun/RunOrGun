using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CharacterRegistry characterRegistry;
    private EnemyStats enemyStats;

    public StateMachine StateMachine { get; private set; }
    public IROGState EnemySpawnState { get; private set; }
    public IROGState EnemyIdleState { get; private set; }
    public IROGState EnemyMoveState { get; private set; }
    public IROGState EnemyAttackState { get; private set; }
    public IROGState EnemyDieState { get; private set; }

    public EnemyVisual Visual { get; private set; }
    public EnemyMovementHandler Movement { get; private set; }
    public EnemyCombatHandler Combat { get; private set; }

    void Awake()
    {
        Visual = GetComponent<EnemyVisual>();
        Movement = GetComponent<EnemyMovementHandler>();
        Combat = GetComponent<EnemyCombatHandler>();
    }

    void Start()
    {
        // 입장시 맵 정보에서 수치 입력 지금은 임시
        enemyStats = new();

        enemyStats.Movement.moveSpeed = 0.5f;
        enemyStats.Movement.rotateSpeed = 30f;
        enemyStats.Movement.sizeRate = 1f;

        StateMachine = new StateMachine();
        EnemySpawnState = new EnemySpawnState(this);
        EnemyIdleState = new EnemyIdleState(this);
        EnemyMoveState = new EnemyMoveState(this);
        EnemyAttackState = new EnemyAttackState(this);
        EnemyDieState = new EnemyDieState(this);
        StateMachine.ChangeState(EnemySpawnState);
    }

    void Update()
    {
        StateMachine.Update();
    }

    public void Initialize(CharacterRegistry registry)
    {
        characterRegistry = registry;
    }
}
