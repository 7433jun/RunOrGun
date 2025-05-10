using UnityEngine;

public class PlayerMoveState : IROGState
{
    private Player player;

    public PlayerMoveState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        Debug.Log("MOVE");
    }

    public void OnUpdate()
    {
        Vector2 input = player.MoveInput;
        Vector3 dir = Vector3.right * input.x + Vector3.forward * input.y;
        dir.y = 0;

        if (dir.magnitude > 0)
        {
            player.Controller.Move(dir * player.moveSpeed * Time.deltaTime);
            Quaternion dirQuat = Quaternion.LookRotation(dir);
            Quaternion nextQuat = Quaternion.Slerp(player.transform.rotation, dirQuat, player.rotateSpeed * Time.deltaTime);
            player.transform.rotation = nextQuat;
        }

        if (input == Vector2.zero)
            player.StateMachine.ChangeState(player.PlayerIdleState);
    }

    public void OnExit()
    {

    }
}
