using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefaultMoveSO", menuName = "Scriptable Objects/Enemy/Behavior/EnemyDefaultMoveSO")]
public class EnemyDefaultMoveSO : EnemyMoveBehaviorSO
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
