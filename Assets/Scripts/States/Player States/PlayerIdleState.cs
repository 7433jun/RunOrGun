using UnityEngine;

public class PlayerIdleState : IROGState
{
    private Player player;

    public PlayerIdleState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        Debug.Log("IDLE");
    }

    public void OnUpdate()
    {
        if (player.MoveInput != Vector2.zero)
            player.StateMachine.ChangeState(player.PlayerMoveState);

        //Enemy enemy = TargetManager.Instance.GetClosestEnemy();
        Enemy enemy = ROGUtility.GetClosestEnemy(CharacterRegistry.Instance.Player, CharacterRegistry.Instance.Enemies);

        if (enemy != null)
        {
            Vector3 dir = Vector3.right * (enemy.transform.position.x - player.transform.position.x) + Vector3.forward * (enemy.transform.position.z - player.transform.position.z);
            dir.y = 0;

            Quaternion dirQuat = Quaternion.LookRotation(dir);
            Quaternion nextQuat = Quaternion.Slerp(player.transform.rotation, dirQuat, player.rotateSpeed * Time.deltaTime);
            player.transform.rotation = nextQuat;
        }
    }

    public void OnExit()
    {

    }
}
