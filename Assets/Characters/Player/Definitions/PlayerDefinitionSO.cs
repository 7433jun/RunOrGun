using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDefinitionSO", menuName = "Scriptable Objects/Player/PlayerDefinitionSO")]
public class PlayerDefinitionSO : ScriptableObject
{
    [SerializeField] private int playerCharacterId;
    [SerializeField] private string playerCharacterName;
    [SerializeField] private PlayerResourceSO playerResourceSO;
    [SerializeField] private PlayerSpawnBehaviorSO playerSpawnBehaviorSO;
    [SerializeField] private PlayerIdleBehaviorSO playerIdleBehaviorSO;
    [SerializeField] private PlayerMoveBehaviorSO playerMoveBehaviorSO;
    [SerializeField] private PlayerAttackBehaviorSO playerAttackBehaviorSO;
    [SerializeField] private PlayerDieBehaviorSO playerDieBehaviorSO;

    public int CharacterID => playerCharacterId;
    public string CharacterName => playerCharacterName;
    public PlayerResourceSO ResourceSO => playerResourceSO;
    public PlayerSpawnBehaviorSO SpawnSO => playerSpawnBehaviorSO;
    public PlayerIdleBehaviorSO IdleSO => playerIdleBehaviorSO;
    public PlayerMoveBehaviorSO MoveSO => playerMoveBehaviorSO;
    public PlayerAttackBehaviorSO AttackSO => playerAttackBehaviorSO;
    public PlayerDieBehaviorSO DieSO => playerDieBehaviorSO;
}
