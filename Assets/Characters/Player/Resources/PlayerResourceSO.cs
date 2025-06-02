using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerResourceSO", menuName = "Scriptable Objects/Player/PlayerResourceSO")]
public class PlayerResourceSO : ScriptableObject
{
    [SerializeField] private PlayerModel playerModel;
    [SerializeField] private ProjectileModel projectileModel;
    [SerializeField] private ProjectileData projectileData;

    public PlayerModel PlayerModel => playerModel;
    public ProjectileModel ProjectileModel => projectileModel;
    public ProjectileData ProjectileData => projectileData;
}

[Serializable]
public class PlayerModel
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector3 pos;
    [SerializeField] private Vector3 rot;
    [SerializeField] private Vector3 scale;
    [SerializeField] private Vector3 muzzleOffset;

    public GameObject Prefab => prefab;
    public Vector3 Pos => pos;
    public Vector3 Rot => rot;
    public Vector3 Scale => scale;
    public Vector3 MuzzleOffset => muzzleOffset;
}

[Serializable]
public class ProjectileModel
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector3 pos;
    [SerializeField] private Vector3 rot;
    [SerializeField] private Vector3 scale;

    public GameObject Prefab => prefab;
    public Vector3 Pos => pos;
    public Vector3 Rot => rot;
    public Vector3 Scale => scale;
}

[Serializable]
public class ProjectileData
{
    // �̰� �⺻ ����ü �������ε� �ٸ����� �����ؾ��� �ƴϴ� �ƴѰ�? ����ü�� ������ �ϳ��ϱ� ���� ���� ������ ������ �ΰ� �������ұ�?
    // ��� �⺻ ����ü ������, �� ������, ���� ������ �δ°� ��������?
    // ����, ���߽� ����, �ݻ� �̷� �ɼǵ��� ��� ��������? -> �ɼǵ��� ������Ʈ�� �����տ� ���̴� ����

    [SerializeField] private GameObject prefab;
    [SerializeField] private float colliderRadius;
    [SerializeField] private float colliderHeight;

    public GameObject Prefab => prefab;
    public float ColliderRadius => colliderRadius;
    public float ColliderHeight => colliderHeight;
}