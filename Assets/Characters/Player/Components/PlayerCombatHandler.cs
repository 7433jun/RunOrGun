using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;

    private float lastAttackTime;

    public bool CanAttack() => Time.time >= lastAttackTime + attackCooldown;

    public void TryAttack(Enemy enemy)
    {
        if (enemy == null) return;
        float dist = Vector3.Distance(transform.position, enemy.transform.position);
        if (dist > attackRange || !CanAttack()) return;

        //enemy.takeDamage(damage);
        lastAttackTime = Time.time;
    }
}
