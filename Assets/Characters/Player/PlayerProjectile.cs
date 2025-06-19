using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Vector3 sizeBase;

    // �ʱ� Ʈ������ �� �����ͼ� base�� �����ϱ�

    [SerializeField] private float sizeRate;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private int bounceWall;
    [SerializeField] private int bounceEnemy;
    [SerializeField] private int pierceEnemy;

    [SerializeField] private float lifeTimer;

    Queue<GameObject> pool;
    private bool isReturned = false;

    

    public void Initilize(PlayerProjectileStats projectileStats, Queue<GameObject> pool)
    {
        // ����ü ���Ȱ� ����
        this.sizeRate = projectileStats.SizeRatio;
        this.speed = projectileStats.Speed;
        this.lifeTime = projectileStats.LifeTime;
        bounceWall = projectileStats.bounceWall;
        bounceEnemy = projectileStats.bounceEnemy;
        pierceEnemy = projectileStats.pierceEnemy;

        transform.localScale = sizeBase * sizeRate;

        this.pool = pool;
        lifeTimer = 0f;
        isReturned = false;
    }

    private void Awake()
    {
        sizeBase = transform.localScale;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            ReturnProjectile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            ReturnProjectile();
        }
        else if (other.TryGetComponent<Wall>(out var wall))
        {
            Debug.Log($"{lifeTimer} {lifeTime}");
            ReturnProjectile();
        }
    }

    private void ReturnProjectile()
    {
        if (isReturned) return;

        isReturned = true;
        lifeTimer = 0f;
        gameObject.SetActive(false);
        pool.Enqueue(gameObject);
    }
}
