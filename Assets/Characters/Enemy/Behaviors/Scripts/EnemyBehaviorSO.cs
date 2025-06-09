using UnityEngine;

public abstract class EnemyBehaviorSO : ScriptableObject
{
    public abstract void InitBehavior(Enemy enemy);
    public abstract void EnterBehavior();
    public abstract void UpdateBehavior();
    public abstract void ExitBehavior();
}

public abstract class EnemySpawnBehaviorSO : EnemyBehaviorSO { }
public abstract class EnemyIdleBehaviorSO : EnemyBehaviorSO { }
public abstract class EnemyMoveBehaviorSO : EnemyBehaviorSO { }
public abstract class EnemyAttackBehaviorSO : EnemyBehaviorSO { }
public abstract class EnemyDieBehaviorSO : EnemyBehaviorSO { }