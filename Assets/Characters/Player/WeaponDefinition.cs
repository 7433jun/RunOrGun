using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDefinition", menuName = "Scriptable Objects/WeaponDefinition")]
public class WeaponDefinition : ScriptableObject
{
    public string weaponId;
    public WeaponData weaponData;
    public ProjectileData projectileData;
    public GameObject projectilePrefab;
}

[Serializable]
public class WeaponData
{
    public GameObject prefab;
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;
    public Vector3 muzzleOffset;
}

[Serializable]
public class ProjectileData
{
    public GameObject prefab;
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;
}