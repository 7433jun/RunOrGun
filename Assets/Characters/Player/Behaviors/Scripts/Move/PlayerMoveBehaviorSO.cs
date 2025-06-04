using UnityEngine;

public abstract class PlayerMoveBehaviorSO : ScriptableObject
{
    public abstract void OnEnter();
    public abstract void OnUpdate(Vector2 input, Transform playerTransform, CharacterController playerController, PlayerMovementStats movementStats);
    public abstract void OnExit();
}
