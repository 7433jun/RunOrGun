using UnityEngine;

public abstract class PlayerBehaviorSO : ScriptableObject
{
    public abstract void InitBehavior(Player player);
    public abstract void EnterBehavior();
    public abstract void UpdateBehavior();
    public abstract void ExitBehavior();
}

public abstract class PlayerSpawnBehaviorSO : PlayerBehaviorSO { }
public abstract class PlayerIdleBehaviorSO : PlayerBehaviorSO { }
public abstract class PlayerMoveBehaviorSO : PlayerBehaviorSO { }
public abstract class PlayerAttackBehaviorSO : PlayerBehaviorSO { }
public abstract class PlayerDieBehaviorSO : PlayerBehaviorSO { }