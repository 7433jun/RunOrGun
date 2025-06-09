using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefinitionSO", menuName = "Scriptable Objects/Enemy/EnemyDefinitionSO")]
public class EnemyDefinitionSO : ScriptableObject
{
    [SerializeField] private int enemyCharacterId;
    [SerializeField] private string enemyCharacterName;
    [SerializeField] private EnemyResourceSO enemyResourceSO;
    [SerializeField] private EnemySpawnBehaviorSO enemySpawnBehaviorSO;
    [SerializeField] private EnemyIdleBehaviorSO enemyIdleBehaviorSO;
    [SerializeField] private EnemyMoveBehaviorSO enemyMoveBehaviorSO;
    [SerializeField] private EnemyAttackBehaviorSO enemyAttackBehaviorSO;
    [SerializeField] private EnemyDieBehaviorSO enemyDieBehaviorSO;

    public int EnemyID => enemyCharacterId;
    public string EnemyName => enemyCharacterName;
    public EnemyResourceSO ResourceSO => enemyResourceSO;
    public EnemySpawnBehaviorSO SpawnSO => enemySpawnBehaviorSO;
    public EnemyIdleBehaviorSO IdleSO => enemyIdleBehaviorSO;
    public EnemyMoveBehaviorSO MoveSO => enemyMoveBehaviorSO;
    public EnemyAttackBehaviorSO AttackSO => enemyAttackBehaviorSO;
    public EnemyDieBehaviorSO DieSO => enemyDieBehaviorSO;
}
