using UnityEngine;

public class EnemyDefaultSpawn : EnemySpawnBehavior
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
