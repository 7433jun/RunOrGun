using UnityEngine;

public struct PlayerAttackContext
{
    public readonly Enemy Enemy;
    public readonly Transform PlayerTransform;
    public readonly CharacterController PlayerController;
    public readonly PlayerStats PlayerStats;
    public readonly PlayerResourceSO PlayerResourceSO;

    public PlayerAttackContext(
        Enemy enemy,
        Transform playerTransform,
        CharacterController playerController,
        PlayerStats playerStats,
        PlayerResourceSO playerResourceSO
        )
    {
        Enemy = enemy;
        PlayerTransform = playerTransform;
        PlayerController = playerController;
        PlayerStats = playerStats;
        PlayerResourceSO = playerResourceSO;
    }
}
