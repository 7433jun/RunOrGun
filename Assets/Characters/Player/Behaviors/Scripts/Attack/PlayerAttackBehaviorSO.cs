using UnityEngine;

public abstract class PlayerAttackBehaviorSO : ScriptableObject
{
    public abstract void InitBehavior(Player player);
    public abstract void EnterBehavior();
    public abstract void Attack();
    public abstract void ExitBehavior();
}
