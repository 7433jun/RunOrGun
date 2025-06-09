using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefaultDieSO", menuName = "Scriptable Objects/Enemy/Behavior/EnemyDefaultDieSO")]
public class EnemyDefaultDieSO : EnemyDieBehaviorSO
{
    private Enemy enemy;

    public override void InitBehavior(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public override void EnterBehavior()
    {
        enemy.characterRegistry.Unregister(enemy);
    }

    public override void UpdateBehavior()
    {

    }

    public override void ExitBehavior()
    {
        enemy.gameObject.SetActive(false);
    }
}
