using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDefinition", menuName = "Scriptable Objects/PlayerDefinition")]
public class PlayerDefinition : ScriptableObject
{
    public int playerCharacterId;
    public string playerCharacterName;

    // 리소스 (모델, 애니메이션, 사운드)
    // 스탯은 로비에서 받아온 스탯 적용(없음)
    // 비헤이비어

    public PlayerResourceSO playerResourceSO;







    public WeaponModel weaponModel;
    public ProjectileModel projectileModel;
    public ProjectileData projectileData;
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
public class ProjectileModel
{
    public GameObject prefab;
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;
}

[Serializable]
public class ProjectileData
{
    public GameObject prefab;
    public float colliderRadius;
    public float colliderHeight;
}