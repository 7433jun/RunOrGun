using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager Instance { get; private set; }

    private Player player;
    private List<Enemy> enemies = new();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterPlayer(Player player) => this.player = player;
    public void UnregisterPlayer() => this.player = null;
    public void RegisterEnemy(Enemy enemy) => enemies.Add(enemy);
    public void UnregisterEnemy(Enemy enemy) => enemies.Remove(enemy);

    public Enemy GetClosestEnemy()
    {
        if (player == null || enemies.Count == 0)
            return null;

        Enemy closestEnemy = null;
        Vector3 playerPos = player.transform.position;
        float minSqrDistance = float.MaxValue;

        foreach (Enemy enemy in enemies)
        {
            Vector3 enemyPos = enemy.transform.position;
            float dx = playerPos.x - enemyPos.x;
            float dz = playerPos.z - enemyPos.z;
            float sqrDistance = dx * dx + dz * dz;

            if (sqrDistance < minSqrDistance)
            {
                minSqrDistance = sqrDistance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
