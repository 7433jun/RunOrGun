using System;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    public EnemyMovementStats Movement = new();
}

[Serializable]
public class EnemyMovementStats
{
    public float moveSpeed;
    public float rotateSpeed;
    public float sizeRate; // ��Ʈ�ڽ� ����
}