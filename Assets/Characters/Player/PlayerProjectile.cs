using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private GameObject model;

    // 초기 트랜스폼 값 가져와서 base값 설정하기

    private float sizeRate;
    private float speed;
    private float lifeTime;
    private int bounceWall;
    private int bounceEnemy;
    private int pierceEnemy;

    private float lifeTimer;

    public void Initilize(PlayerProjectileStats projectileStats)
    {
        // 투사체 스탯값 기입
        this.sizeRate = projectileStats.sizeRate;
        this.speed = projectileStats.speed * projectileStats.speedRate;
        this.lifeTime = projectileStats.lifeTime * projectileStats.lifeTimeRate;
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
