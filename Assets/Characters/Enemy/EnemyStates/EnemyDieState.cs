using UnityEngine;

public class EnemyDieState : IROGState
{
    private Enemy enemy;

    public EnemyDieState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        enemy.characterRegistry.Unregister(enemy);

        enemy.gameObject.SetActive(false);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}
