using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyID;
    public string enemyName;

    public CharacterRegistry characterRegistry;

    public EnemyStats enemyStats;

    public EnemyVisual Visual { get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }

    void Awake()
    {
        Visual = GetComponent<EnemyVisual>();
        StateMachine = GetComponent<EnemyStateMachine>();
    }

    void Start()
    {
        // 입장시 맵 정보에서 수치 입력 지금은 임시
        enemyStats = new();

        enemyStats.Movement.moveSpeed = 0.5f;
        enemyStats.Movement.rotateSpeed = 30f;
        enemyStats.Movement.sizeRate = 1f;

        //Visual.Initialize();

        StateMachine.Initialize(this);
    }

    public void Initialize(CharacterRegistry registry)
    {
        characterRegistry = registry;
    }

    // 임시
    private void OnDisable()
    {
        characterRegistry.Unregister(this);
    }
}
