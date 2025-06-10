using UnityEngine;

public abstract class PlayerBehavior : MonoBehaviour
{
    public abstract void InitBehavior(Player player);
    public abstract void EnterBehavior();
    public abstract void UpdateBehavior();
    public abstract void ExitBehavior();
}

public abstract class PlayerSpawnBehavior : PlayerBehavior { }
public abstract class PlayerIdleBehavior : PlayerBehavior { }
public abstract class PlayerMoveBehavior : PlayerBehavior { }
public abstract class PlayerAttackBehavior : PlayerBehavior { }
public abstract class PlayerDieBehavior : PlayerBehavior { }