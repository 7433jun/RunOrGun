using System.Collections.Generic;
using UnityEngine;

public class ROGUtility
{
    public static Enemy GetClosestEnemy(Player player, List<Enemy> enemies)
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
