using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDefinition", menuName = "Scriptable Objects/WeaponDefinition")]
public class WeaponDefinition : ScriptableObject
{
    public string weaponId;
    public GameObject weaponPrefab;
    public Vector3 muzzleOffset;
    public GameObject projectilePrefab;
}
