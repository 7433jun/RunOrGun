using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private List<EnemyDefinition> enemyDefinitionList;

    // 이것들 다 패턴으로 등록하던가 시드에 따른 랜덤생성해야됨 테스트용으로 직접등록함
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private List<EnemySpawnData> enemySpawnDataList;

    private Dictionary<int, EnemyDefinition> enemyDictionary;

    public void Initialize()
    {
        enemyDictionary = new();

        foreach (EnemyDefinition enemyDefinition in enemyDefinitionList)
        {
            enemyDictionary.Add(enemyDefinition.enemyId, enemyDefinition);
        }
    }

    public Player SpawnPlayer()
    {
        GameObject gameObject = Instantiate(playerPrefab, playerPos, Quaternion.identity);
        return gameObject.GetComponent<Player>();
    }

    public List<Enemy> SpawnEnemy()
    {
        List<Enemy> list = new();

        foreach (EnemySpawnData data in enemySpawnDataList)
        {
            GameObject gameObject = Instantiate(enemyPrefab, data.pos, Quaternion.Euler(data.rot));
            gameObject.transform.localScale = data.scale;
            list.Add(gameObject.GetComponent<Enemy>());
        }
        return list;
    }
}

[Serializable]
public class EnemySpawnData
{
    public int enemyId;
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;
}