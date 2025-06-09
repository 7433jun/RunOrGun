using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRangedAttackSO", menuName = "Scriptable Objects/Enemy/Behavior/EnemyRangedAttackSO")]
public class EnemyRangedAttackSO : EnemyAttackBehaviorSO
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
