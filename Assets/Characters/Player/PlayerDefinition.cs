using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDefinition", menuName = "Scriptable Objects/PlayerDefinition")]
public class PlayerDefinition : ScriptableObject
{
    public int playerCharacterId;
    public string playerCharacterName;

    // ���ҽ� (��, �ִϸ��̼�, ����)
    // ������ �κ񿡼� �޾ƿ� ���� ����(����)
    // �����̺��

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