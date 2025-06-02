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
        // ���� ���ǵ� �ݶ��̴� ũ��� ����
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.radius = resource.ProjectileData.ColliderRadius;
        capsuleCollider.height = resource.ProjectileData.ColliderHeight;

        // ������ �� ������ ����, ��ġ ����
        model = Instantiate(resource.ProjectileModel.Prefab, transform);
        model.transform.localPosition = resource.ProjectileModel.Pos;
        model.transform.localRotation = Quaternion.Euler(resource.ProjectileModel.Rot);
        model.transform.localScale = resource.ProjectileModel.Scale;

        // ����ü ���Ȱ� ����
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
