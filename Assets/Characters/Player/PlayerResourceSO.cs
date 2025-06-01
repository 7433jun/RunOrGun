using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerResourceSO", menuName = "Scriptable Objects/Player/PlayerResourceSO")]
public class PlayerResourceSO : ScriptableObject
{
    
}

//[Serializable]
//public class PlayerModel
//{
//    public GameObject prefab;
//    public Vector3 pos;
//    public Vector3 rot;
//    public Vector3 scale;
//    public Vector3 muzzleOffset;
//}
//
//[Serializable]
//public class ProjectileModel
//{
//    public GameObject prefab;
//    public Vector3 pos;
//    public Vector3 rot;
//    public Vector3 scale;
//}
//
//[Serializable]
//public class ProjectileData
//{
//    public GameObject prefab; // 이거 기본 투사체 프리팹인데 다른곳에 구성해야지 아니다 아닌가? 투사체의 로직은 하나니까 굳이 여러 로직을 염두해 두고 만들어야할까?
//    // 어딘가 기본 투사체 프리팹, 빔 프리팹, 근접 프리팹 두는게 맞으려나?
//    // 유도, 적중시 폭발, 반사 이런 옵션들은 어떻게 구성하지? -> 옵션들은 컴포넌트로 프리팹에 붙이는 방향
//    public float colliderRadius;
//    public float colliderHeight;
//}