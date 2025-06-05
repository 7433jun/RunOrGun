using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputMoveSO", menuName = "Scriptable Objects/PlayerInputMoveSO")]
public class PlayerInputMoveSO : PlayerMoveBehaviorSO
{
    public override void EnterBehavior(PlayerMoveContext ctx)
    {
        // 시작시 초기화 할거 세팅
    }

    public override void Move(PlayerMoveContext ctx)
    {
        // 방향
        Vector3 dir = new Vector3(ctx.PlayerInput.x, 0f, ctx.PlayerInput.y);

        if (dir.magnitude > 0)
        {
            // 이동
            ctx.PlayerController.Move(dir * ctx.PlayerMoveStats.moveSpeed * Time.deltaTime);

            // 회전
            Quaternion dirQuat = Quaternion.LookRotation(dir);
            Quaternion nextQuat = Quaternion.Slerp(ctx.PlayerTransform.rotation, dirQuat, ctx.PlayerMoveStats.rotateSpeed * Time.deltaTime);
            ctx.PlayerTransform.rotation = nextQuat;
        }
    }

    public override void ExitBehavior(PlayerMoveContext ctx)
    {
        // 애니메이션 종료함수같은거 세팅
    }
}
