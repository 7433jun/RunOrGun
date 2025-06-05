using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputMoveSO", menuName = "Scriptable Objects/PlayerInputMoveSO")]
public class PlayerInputMoveSO : PlayerMoveBehaviorSO
{
    public override void EnterBehavior(PlayerMoveContext ctx)
    {
        // ���۽� �ʱ�ȭ �Ұ� ����
    }

    public override void Move(PlayerMoveContext ctx)
    {
        // ����
        Vector3 dir = new Vector3(ctx.PlayerInput.x, 0f, ctx.PlayerInput.y);

        if (dir.magnitude > 0)
        {
            // �̵�
            ctx.PlayerController.Move(dir * ctx.PlayerMoveStats.moveSpeed * Time.deltaTime);

            // ȸ��
            Quaternion dirQuat = Quaternion.LookRotation(dir);
            Quaternion nextQuat = Quaternion.Slerp(ctx.PlayerTransform.rotation, dirQuat, ctx.PlayerMoveStats.rotateSpeed * Time.deltaTime);
            ctx.PlayerTransform.rotation = nextQuat;
        }
    }

    public override void ExitBehavior(PlayerMoveContext ctx)
    {
        // �ִϸ��̼� �����Լ������� ����
    }
}
