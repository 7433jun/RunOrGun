using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefaultSpawnSO", menuName = "Scriptable Objects/Enemy/Behavior/EnemyDefaultSpawnSO")]
public class EnemyDefaultSpawnSO : EnemySpawnBehaviorSO
{
    private Enemy enemy;

    public override void InitBehavior(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public override void EnterBehavior()
    {

    }

    public override void UpdateBehavior()
    {

    }

    public override void ExitBehavior()
    {

    }
}
