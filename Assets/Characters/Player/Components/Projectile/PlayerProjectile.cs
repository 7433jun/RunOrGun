using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Vector3 sizeBase;
    private Vector3 direction;

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

    

    public void Initilize(PlayerProjectileStats projectileStats, Vector3 direction, Queue<GameObject> pool)
    {
        // ����ü ���Ȱ� ����
        this.sizeRate = projectileStats.SizeRatio;
        this.speed = projectileStats.Speed;
        this.lifeTime = projectileStats.LifeTime;
        bounceWall = projectileStats.bounceWall;
        bounceEnemy = projectileStats.bounceEnemy;
        pierceEnemy = projectileStats.pierceEnemy;

        transform.localScale = sizeBase * sizeRate;

        this.direction = direction.normalized;

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
        transform.position += direction * speed * Time.deltaTime;

        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            ReturnProjectile();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            // �� �ݻ�
            if (bounceEnemy > 0)
            {

            }
            // �� ����
            if (pierceEnemy > 0)
            {

            }
            ReturnProjectile();
        }
        else if (collision.gameObject.TryGetComponent<Wall>(out var wall))
        {
            // �� �ݻ�, ���� ������Ʈ �и��� ���߿� ���� �̺�Ʈ �������� ��� �ϸ� �ɰŰ�����
            if (bounceWall > 0)
            {
                Vector3 normal = collision.contacts[0].normal;
                direction = Vector3.Reflect(direction, normal).normalized;
                transform.rotation = Quaternion.LookRotation(direction);
                bounceWall--;
                return;
            }
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
