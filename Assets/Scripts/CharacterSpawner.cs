using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    // 이것들 다 패턴으로 등록하던가 시드에 따른 랜덤생성해야됨 테스트용으로 직접등록함
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
