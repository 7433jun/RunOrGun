using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefaultIdleSO", menuName = "Scriptable Objects/Enemy/Behavior/EnemyDefaultIdleSO")]
public class EnemyDefaultIdleSO : EnemyIdleBehaviorSO
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
