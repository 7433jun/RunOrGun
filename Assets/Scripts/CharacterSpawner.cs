using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    // �̰͵� �� �������� ����ϴ��� �õ忡 ���� ���������ؾߵ� �׽�Ʈ������ ���������
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private List<Vector3> enemyPosList;

    public Player SpawnPlayer()
    {
        GameObject gameObject = Instantiate(playerPrefab, playerPos, Quaternion.identity);
        return gameObject.GetComponent<Player>();
    }

    public List<Enemy> SpawnEnemy()
    {
        List<Enemy> list = new();

        foreach (Vector3 pos in enemyPosList)
        {
            GameObject gameObject = Instantiate(enemyPrefab, pos, Quaternion.identity);
            list.Add(gameObject.GetComponent<Enemy>());
        }
        return list;
    }
}
