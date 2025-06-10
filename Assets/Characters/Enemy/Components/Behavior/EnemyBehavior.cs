using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    public abstract void InitBehavior(Enemy enemy);
    public abstract void EnterBehavior();
    public abstract void UpdateBehavior();
    public abstract void ExitBehavior();
}

public abstract class EnemySpawnBehavior : EnemyBehavior { }
public abstract class EnemyIdleBehavior : EnemyBehavior { }
public abstract class EnemyMoveBehavior : EnemyBehavior { }
public abstract class EnemyAttackBehavior : EnemyBehavior { }
public abstract class EnemyDieBehavior : EnemyBehavior { }