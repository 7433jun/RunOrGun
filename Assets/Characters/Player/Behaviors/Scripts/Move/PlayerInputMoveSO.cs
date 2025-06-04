using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputMoveSO", menuName = "Scriptable Objects/PlayerInputMoveSO")]
public class PlayerInputMoveSO : PlayerMoveBehaviorSO
{
    public override void OnEnter()
    {
        // 시작시 초기화 할거 세팅
    }

    public override void OnUpdate(Vector2 input, Transform playerTransform, CharacterController playerController, PlayerMovementStats movementStats)
    {
        // 방향
        Vector3 dir = new Vector3(input.x, 0f, input.y);

        if (dir.magnitude > 0)
        {
            // 이동
            playerController.Move(dir * movementStats.moveSpeed * Time.deltaTime);

            // 회전
            Quaternion dirQuat = Quaternion.LookRotation(dir);
            Quaternion nextQuat = Quaternion.Slerp(playerTransform.rotation, dirQuat, movementStats.rotateSpeed * Time.deltaTime);
            playerTransform.rotation = nextQuat;
        }
    }

    public override void OnExit()
    {
        // 애니메이션 종료함수같은거 세팅
    }
}
