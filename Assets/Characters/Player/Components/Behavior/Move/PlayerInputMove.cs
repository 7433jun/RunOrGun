using UnityEngine;

public class PlayerInputMove : PlayerMoveBehavior
{
    private Player player;
    private Transform playerTransform;
    private CharacterController playerController;
    private PlayerStats playerStats;

    public override void InitBehavior(Player player)
    {
        this.player = player;
        playerTransform = player.transform;
        playerController = player.GetComponent<CharacterController>();
        playerStats = player.statsSystem.Stats;
    }

    public override void EnterBehavior()
    {
        // 시작시 초기화 할거 세팅
    }

    public override void UpdateBehavior()
    {
        // 방향
        Vector3 dir = new Vector3(player.MoveInput.x, 0f, player.MoveInput.y);

        if (dir.magnitude > 0)
        {
            // 회전
            Quaternion dirQuat = Quaternion.LookRotation(dir);
            Quaternion nextQuat = Quaternion.Slerp(playerTransform.rotation, dirQuat, playerStats.Move.rotateSpeed * Time.deltaTime);
            playerTransform.rotation = nextQuat;

            // 이동
            playerController.Move(dir * playerStats.Move.moveSpeedCurrent * Time.deltaTime);
        }
    }

    public override void ExitBehavior()
    {
        // 애니메이션 종료함수같은거 세팅
    }
}
