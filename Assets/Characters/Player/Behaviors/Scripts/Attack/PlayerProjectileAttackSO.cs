using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProjectileAttackSO", menuName = "Scriptable Objects/PlayerProjectileAttackSO")]
public class PlayerProjectileAttackSO : PlayerAttackBehaviorSO
{
    public override void EnterBehavior(PlayerAttackContext ctx)
    {
        
    }

    public override void Attack(PlayerAttackContext ctx)
    {
        
        // 에너미 위치로 방향백터 계산
        Vector3 projectileSpawnPos = ctx.PlayerTransform.TransformPoint(ctx.PlayerResourceSO.PlayerModel.MuzzleOffset);
        Vector3 direction = ctx.Enemy.transform.position - projectileSpawnPos;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);

        // 투사체 생성, 초기화 (투사체에 옵션 붙을수도있으니 별개 함수로 빼줘야할듯)
        GameObject projectileObj = Instantiate(ctx.PlayerResourceSO.ProjectileData.Prefab, projectileSpawnPos, rotation);
        var projectile = projectileObj.GetComponent<PlayerProjectile>();
        projectile.Initilize(ctx.PlayerResourceSO, ctx.PlayerStats.Projectile);

        //float dist = Vector3.Distance(transform.position, enemy.transform.position);
        //if (dist > stats.Attack.range || !CanAttack()) return;

        //enemy.takeDamage(damage);

    }

    public override void ExitBehavior(PlayerAttackContext ctx)
    {
        
    }
}
