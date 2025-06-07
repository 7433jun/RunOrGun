using UnityEngine;

public abstract class PlayerMoveBehaviorSO : ScriptableObject
{
    public abstract void InitBehavior(Player player);
    public abstract void EnterBehavior();
    public abstract void Move();
    public abstract void ExitBehavior();
}
