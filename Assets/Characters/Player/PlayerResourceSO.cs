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
//    public GameObject prefab; // �̰� �⺻ ����ü �������ε� �ٸ����� �����ؾ��� �ƴϴ� �ƴѰ�? ����ü�� ������ �ϳ��ϱ� ���� ���� ������ ������ �ΰ� �������ұ�?
//    // ��� �⺻ ����ü ������, �� ������, ���� ������ �δ°� ��������?
//    // ����, ���߽� ����, �ݻ� �̷� �ɼǵ��� ��� ��������? -> �ɼǵ��� ������Ʈ�� �����տ� ���̴� ����
//    public float colliderRadius;
//    public float colliderHeight;
//}