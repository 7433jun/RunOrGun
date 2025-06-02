using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDefinitionSO", menuName = "Scriptable Objects/PlayerDefinitionSO")]
public class PlayerDefinitionSO : ScriptableObject
{
    [SerializeField] private int playerCharacterId;
    [SerializeField] private string playerCharacterName;
    [SerializeField] private PlayerResourceSO playerResourceSO;
    [SerializeField] private PlayerBehaviorSO playerBehaviourSO;

    public int CharacterID => playerCharacterId;
    public string CharacterName => playerCharacterName;
    public PlayerResourceSO ResourceSO => playerResourceSO;
    public PlayerBehaviorSO BehaviorSO => playerBehaviourSO;
}
