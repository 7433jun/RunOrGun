using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefinition", menuName = "Scriptable Objects/EnemyDefinition")]
public class EnemyDefinition : ScriptableObject
{
    public int enemyId;
    public string enemyName;
    public EnemyModel enemyModel;
    public EnemyProjectileModel enemyProjectileModel;
    public EnemyProjectileData enemyProjectileData;

    public TestInterface testInterface;
}

[Serializable]
public class EnemyModel
{
    public GameObject prefab;
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;
}

[Serializable]
public class EnemyProjectileModel
{
    public GameObject prefab;
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;
}

[Serializable]
public class EnemyProjectileData
{
    public GameObject prefab;
    public float colliderRadius;
    public float colliderHeight;
}

public interface TestInterface
{ }