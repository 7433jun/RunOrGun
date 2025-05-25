using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    // �̰͵� �� �������� ����ϴ��� �õ忡 ���� ���������ؾߵ� �׽�Ʈ������ ���������
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private List<Vector3> enemyPosList;

    public void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerPos, Quaternion.identity);
    }

    public void SpawnEnemy()
    {
        if (enemyPosList.Count == 0) return;

        foreach (Vector3 pos in enemyPosList)
        {
            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
}
