using UnityEngine;

public abstract class PlayerAttackBehaviorSO : ScriptableObject
{
    public abstract void EnterBehavior(PlayerAttackContext ctx);
    public abstract void Attack(PlayerAttackContext ctx);
    public abstract void ExitBehavior(PlayerAttackContext ctx);
}
