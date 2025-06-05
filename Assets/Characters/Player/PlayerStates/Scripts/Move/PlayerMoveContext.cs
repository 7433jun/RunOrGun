using UnityEngine;

public struct PlayerMoveContext
{
    public readonly Vector2 PlayerInput;
    public readonly Transform PlayerTransform;
    public readonly CharacterController PlayerController;
    public readonly PlayerMovementStats PlayerMoveStats;

    public PlayerMoveContext(
        Vector2 playerInput,
        Transform playerTransform,
        CharacterController playerController,
        PlayerMovementStats playerMoveStats
        )
    {
        PlayerInput = playerInput;
        PlayerTransform = playerTransform;
        PlayerController = playerController;
        PlayerMoveStats = playerMoveStats;
    }
}
