using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private List<PlayerDefinitionSO> playerDefinitionList;
    [SerializeField] private List<EnemyDefinitionSO> enemyDefinitionList;

    // 이것들 다 패턴으로 등록하던가 시드에 따른 랜덤생성해야됨 테스트용으로 직접등록함
    [SerializeField] private PlayerSpawnData playerSpawnData;
    [SerializeField] private List<EnemySpawnData> enemySpawnDataList;

    private Dictionary<int, PlayerDefinitionSO> playerDictionary;
    private Dictionary<int, EnemyDefinitionSO> enemyDictionary;

    public void Initialize()
    {
        playerDictionary = new();
        enemyDictionary = new();

        foreach (PlayerDefinitionSO playerDefinition in playerDefinitionList)
        {
            playerDictionary.Add(playerDefinition.CharacterID, playerDefinition);
        }

        foreach (EnemyDefinitionSO enemyDefinition in enemyDefinitionList)
        {
            enemyDictionary.Add(enemyDefinition.EnemyID, enemyDefinition);
        }
    }

    public Player SpawnPlayer()
    {
        GameObject gameObject = Instantiate(playerPrefab, playerSpawnData.pos, Quaternion.Euler(playerSpawnData.rot));
        gameObject.transform.localScale = playerSpawnData.scale;

        Player player = gameObject.GetComponent<Player>();
        player.SetDefinition(playerDictionary[playerSpawnData.playerId]);

        return player;
    }

    public List<Enemy> SpawnEnemy()
    {
        List<Enemy> list = new();

        foreach (EnemySpawnData data in enemySpawnDataList)
        {
            if (!enemyDictionary.ContainsKey(data.enemyId)) continue;

            GameObject gameObject = Instantiate(enemyPrefab, data.pos, Quaternion.Euler(data.rot));
            gameObject.transform.localScale = data.scale;

            Enemy enemy = gameObject.GetComponent<Enemy>();
            enemy.SetDefinition(enemyDictionary[data.enemyId]);

            list.Add(enemy);
        }
        return list;
    }
}

[Serializable]
public class PlayerSpawnData
{
    public int playerId;
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;
}

[Serializable]
public class EnemySpawnData
{
    public int enemyId;
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;
}