using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private GameObject model;

    // �ʱ� Ʈ������ �� �����ͼ� base�� �����ϱ�

    private float sizeRate;
    private float speed;
    private float lifeTime;
    private int bounceWall;
    private int bounceEnemy;
    private int pierceEnemy;

    private float lifeTimer;

    public void Initilize(PlayerProjectileStats projectileStats)
    {
        // ����ü ���Ȱ� ����
        this.sizeRate = projectileStats.SizeRatio;
        this.speed = projectileStats.Speed;
        this.lifeTime = projectileStats.LifeTime;
        bounceWall = projectileStats.bounceWall;
        bounceEnemy = projectileStats.bounceEnemy;
        pierceEnemy = projectileStats.pierceEnemy;

        
    }

    void OnEnable()
    {
        lifeTimer = 0f;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }
}
