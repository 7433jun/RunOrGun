using UnityEngine;

public abstract class PlayerMoveBehaviorSO : ScriptableObject
{
    public abstract void EnterBehavior(PlayerMoveContext ctx);
    public abstract void Move(PlayerMoveContext ctx);
    public abstract void ExitBehavior(PlayerMoveContext ctx);
}
