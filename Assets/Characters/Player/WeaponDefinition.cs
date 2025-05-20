using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDefinition", menuName = "Scriptable Objects/WeaponDefinition")]
public class WeaponDefinition : ScriptableObject
{
    public string weaponId;
    public WeaponModel weaponModel;
    public ProjectileData projectileData;
    public ProjectileModel projectileModel;
}

[Serializable]
public class WeaponModel
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
    public float colliderRadius;
    public float colliderHeight;
}

[Serializable]
public class ProjectileModel
{
    public GameObject prefab;
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;
}