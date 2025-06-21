using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Vector3 sizeBase;
    private Vector3 direction;

    // 초기 트랜스폼 값 가져와서 base값 설정하기

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
        // 투사체 스탯값 기입
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
            // 적 반사
            if (bounceEnemy > 0)
            {

            }
            // 적 관통
            if (pierceEnemy > 0)
            {

            }
            ReturnProjectile();
        }
        else if (collision.gameObject.TryGetComponent<Wall>(out var wall))
        {
            // 벽 반사, 별개 컴포넌트 분리는 나중에 하자 이벤트 구독으로 어떻게 하면 될거같은데
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
