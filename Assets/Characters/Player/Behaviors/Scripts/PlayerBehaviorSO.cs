using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBehaviorSO", menuName = "Scriptable Objects/PlayerBehaviorSO")]
public class PlayerBehaviorSO : ScriptableObject
{
    [SerializeField] private PlayerSpawnBehaviorSO playerSpawnBehaviorSO;
    [SerializeField] private PlayerIdleBehaviorSO playerIdleBehaviorSO;
    [SerializeField] private PlayerMoveBehaviorSO playerMoveBehaviorSO;
    [SerializeField] private PlayerAttackBehaviorSO playerAttackBehaviorSO;
    [SerializeField] private PlayerDieBehaviorSO playerDieBehaviorSO;

    public PlayerSpawnBehaviorSO SpawnSO => playerSpawnBehaviorSO;
    public PlayerIdleBehaviorSO IdleSO => playerIdleBehaviorSO;
    public PlayerMoveBehaviorSO MoveSO => playerMoveBehaviorSO;
    public PlayerAttackBehaviorSO AttackSO => playerAttackBehaviorSO;
    public PlayerDieBehaviorSO DieSO => playerDieBehaviorSO;
}
