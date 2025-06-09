using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerResourceSO", menuName = "Scriptable Objects/Player/PlayerResourceSO")]
public class PlayerResourceSO : ScriptableObject
{
    [SerializeField] private PModel playerModel;
    [SerializeField] private PProjectileModel projectileModel;
    [SerializeField] private PProjectileData projectileData;

    public PModel PlayerModel => playerModel;
    public PProjectileModel ProjectileModel => projectileModel;
    public PProjectileData ProjectileData => projectileData;


    // 모델 데이터뿐아니라 애니메이션, 사운드, 이펙트 다 있어야됨
}

[Serializable]
public class PModel
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
public class PProjectileModel
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
public class PProjectileData
{
    // 이거 기본 투사체 프리팹인데 다른곳에 구성해야지 아니다 아닌가? 투사체의 로직은 하나니까 굳이 여러 로직을 염두해 두고 만들어야할까?
    // 어딘가 기본 투사체 프리팹, 빔 프리팹, 근접 프리팹 두는게 맞으려나?
    // 유도, 적중시 폭발, 반사 이런 옵션들은 어떻게 구성하지? -> 옵션들은 컴포넌트로 프리팹에 붙이는 방향

    [SerializeField] private GameObject prefab;
    [SerializeField] private float colliderRadius;
    [SerializeField] private float colliderHeight;

    public GameObject Prefab => prefab;
    public float ColliderRadius => colliderRadius;
    public float ColliderHeight => colliderHeight;
}