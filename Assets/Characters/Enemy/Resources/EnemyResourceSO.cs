using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyResourceSO", menuName = "Scriptable Objects/Enemy/EnemyResourceSO")]
public class EnemyResourceSO : ScriptableObject
{
    [SerializeField] private EModel enemyModel;
    [SerializeField] private EProjectileModel projectileModel;
    [SerializeField] private EProjectileData projectileData;

    public EModel EnemyModel => enemyModel;
    public EProjectileModel ProjectileModel => projectileModel;
    public EProjectileData ProjectileData => projectileData;
}

[Serializable]
public class EModel
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
public class EProjectileModel
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
public class EProjectileData
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