using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerPrefabList;
    [SerializeField] private List<GameObject> enemyPrefabList;

    // 이것들 다 패턴으로 등록하던가 시드에 따른 랜덤생성해야됨 테스트용으로 직접등록함
    [SerializeField] private SpawnData playerSpawnData;
    [SerializeField] private List<SpawnData> enemySpawnDataList;

    private Dictionary<int, GameObject> playerDictionary;
    private Dictionary<int, GameObject> enemyDictionary;

    public void Initialize()
    {
        playerDictionary = new();
        enemyDictionary = new();

        foreach (GameObject playerPrefab in playerPrefabList)
        {
            int id = playerPrefab.GetComponent<Player>().playerID;
            playerDictionary.Add(id, playerPrefab);
        }

        foreach (GameObject enemyPrefab in enemyPrefabList)
        {
            int id = enemyPrefab.GetComponent<Enemy>().enemyID;
            enemyDictionary.Add(id, enemyPrefab);
        }
    }

    public Player SpawnPlayer()
    {
        if (!playerDictionary.ContainsKey(playerSpawnData.id)) return null;

        GameObject playerObj = Instantiate(playerDictionary[playerSpawnData.id], playerSpawnData.pos, Quaternion.Euler(playerSpawnData.rot));
        playerObj.transform.localScale = playerSpawnData.scale;

        Player player = playerObj.GetComponent<Player>();

        return player;
    }

    public List<Enemy> SpawnEnemy()
    {
        List<Enemy> list = new();

        foreach (SpawnData data in enemySpawnDataList)
        {
            if (!enemyDictionary.ContainsKey(data.id)) continue;

            GameObject enemyObj = Instantiate(enemyDictionary[data.id], data.pos, Quaternion.Euler(data.rot));
            enemyObj.transform.localScale = data.scale;

            Enemy enemy = enemyObj.GetComponent<Enemy>();

            list.Add(enemy);
        }
        return list;
    }
}

[Serializable]
public class SpawnData
{
    public int id;
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;
}