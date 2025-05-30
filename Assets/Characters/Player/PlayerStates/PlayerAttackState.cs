using UnityEngine;

public class PlayerAttackState : IROGState
{
    private Player player;

    public PlayerAttackState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        //Debug.Log("Player Attack");
    }

    public void OnUpdate()
    {
        if (player.MoveInput != Vector2.zero)
        {
            player.StateMachine.ChangeState(player.PlayerMoveState);
            return;
        }

        Enemy enemy = ROGUtility.GetClosestEnemy(player, player.characterRegistry.Enemies);

        if (enemy == null)
        {
            player.StateMachine.ChangeState(player.PlayerIdleState);
            return;
        }
        // DieState 넘길 조건 추가

        Vector3 dir = enemy.transform.position - player.transform.position;
        player.Movement.RotateTowards(dir);
        player.Combat.TryAttack(enemy);
    }

    public void OnExit()
    {

    }
}
