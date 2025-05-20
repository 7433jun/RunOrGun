using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject model;

    private float sizeRate;
    private float speed;
    private float lifeTime;
    private int bounceWall;
    private int bounceEnemy;
    private int pierceEnemy;

    private float lifeTimer;

    public void Initilize(WeaponDefinition weaponDefinition, PlayerProjectileStats projectileStats)
    {
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.radius = weaponDefinition.projectileData.colliderRadius;
        capsuleCollider.height = weaponDefinition.projectileData.colliderHeight;

        model = Instantiate(weaponDefinition.projectileModel.prefab, transform);
        model.transform.localPosition = weaponDefinition.projectileModel.pos;
        model.transform.localRotation = Quaternion.Euler(weaponDefinition.projectileModel.rot);
        model.transform.localScale = weaponDefinition.projectileModel.scale;

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
