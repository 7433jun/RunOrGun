using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProjectileAttackSO", menuName = "Scriptable Objects/PlayerProjectileAttackSO")]
public class PlayerProjectileAttackSO : PlayerAttackBehaviorSO
{
    public override void EnterBehavior(PlayerAttackContext ctx)
    {
        
    }

    public override void Attack(PlayerAttackContext ctx)
    {
        
        // ���ʹ� ��ġ�� ������� ���
        Vector3 projectileSpawnPos = ctx.PlayerTransform.TransformPoint(ctx.PlayerResourceSO.PlayerModel.MuzzleOffset);
        Vector3 direction = ctx.Enemy.transform.position - projectileSpawnPos;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);

        // ����ü ����, �ʱ�ȭ (����ü�� �ɼ� �������������� ���� �Լ��� ������ҵ�)
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
