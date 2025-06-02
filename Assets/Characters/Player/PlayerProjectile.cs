using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private GameObject model;

    private float sizeRate;
    private float speed;
    private float lifeTime;
    private int bounceWall;
    private int bounceEnemy;
    private int pierceEnemy;

    private float lifeTimer;

    public void Initilize(PlayerResourceSO resource, PlayerProjectileStats projectileStats)
    {
        // 모델의 정의된 콜라이더 크기로 조절
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.radius = resource.ProjectileData.ColliderRadius;
        capsuleCollider.height = resource.ProjectileData.ColliderHeight;

        // 하위에 모델 프리팹 생성, 위치 조정
        model = Instantiate(resource.ProjectileModel.Prefab, transform);
        model.transform.localPosition = resource.ProjectileModel.Pos;
        model.transform.localRotation = Quaternion.Euler(resource.ProjectileModel.Rot);
        model.transform.localScale = resource.ProjectileModel.Scale;

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
