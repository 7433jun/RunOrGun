using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Vector3 sizeBase;
    private Vector3 direction;
    private Player player;

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

    private Rigidbody rigidBody;

    public void Initilize(Player player, Vector3 direction, Queue<GameObject> pool)
    {
        this.player = player;
        // ����ü ���Ȱ� ����
        sizeRate = player.Stats.Projectile.SizeRatio;
        speed = player.Stats.Projectile.Speed;
        lifeTime = player.Stats.Projectile.LifeTime;
        bounceWall = player.Stats.Projectile.bounceWall;
        bounceEnemy = player.Stats.Projectile.bounceEnemy;
        pierceEnemy = player.Stats.Projectile.pierceEnemy;

        transform.localScale = sizeBase * sizeRate;

        this.direction = direction.normalized;

        this.pool = pool;
        lifeTimer = 0f;
        isReturned = false;
    }

    private void Awake()
    {
        sizeBase = transform.localScale;
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidBody.linearVelocity = direction * speed;
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            ReturnProjectile();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out var wall))
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            //������ �Լ�
            //Debug.Log(other.name + " hit");

            // �� �ݻ�
            if (bounceEnemy > 0)
            {
                List<Enemy> enemies = new List<Enemy>(player.characterRegistry.Enemies);
                enemies.Remove(enemy);
                if (enemies.Count > 0)
                {
                    Enemy newTargetEnemy = ROGUtility.GetClosestEnemy(transform, enemies);
                    transform.position = enemy.transform.position;
                    direction = (newTargetEnemy.transform.position - enemy.transform.position).normalized;
                    transform.rotation = Quaternion.LookRotation(direction);
                    bounceEnemy--;
                    return;
                }
            }
            // �� ����
            if (pierceEnemy > 0)
            {
                pierceEnemy--;
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
