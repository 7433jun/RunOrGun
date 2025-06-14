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
        // ���۽� �ʱ�ȭ �Ұ� ����
    }

    public override void UpdateBehavior()
    {
        // ����
        Vector3 dir = new Vector3(player.MoveInput.x, 0f, player.MoveInput.y);

        if (dir.magnitude > 0)
        {
            // ȸ��
            Quaternion dirQuat = Quaternion.LookRotation(dir);
            Quaternion nextQuat = Quaternion.Slerp(playerTransform.rotation, dirQuat, playerStats.Move.rotateSpeed * Time.deltaTime);
            playerTransform.rotation = nextQuat;

            // �̵�
            playerController.Move(dir * playerStats.Move.moveSpeedCurrent * Time.deltaTime);
        }
    }

    public override void ExitBehavior()
    {
        // �ִϸ��̼� �����Լ������� ����
    }
}
