using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject model;

    private float sizeRate;
    private float speed;
    private float lifeTime = 1f;
    private int bounceWall;
    private int bounceEnemy;
    private int pierceEnemy;

    private float lifeTimer;

    public void Initilize(WeaponDefinition weaponDefinition, PlayerProjectileStats projectileStats)
    {
        model = Instantiate(weaponDefinition.projectileData.prefab, transform);

        model.transform.localPosition = weaponDefinition.projectileData.pos;
        model.transform.rotation = Quaternion.Euler(weaponDefinition.projectileData.rot);
        model.transform.localScale = weaponDefinition.projectileData.scale;

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
