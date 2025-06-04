using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputMoveSO", menuName = "Scriptable Objects/PlayerInputMoveSO")]
public class PlayerInputMoveSO : PlayerMoveBehaviorSO
{
    public override void OnEnter()
    {
        // ���۽� �ʱ�ȭ �Ұ� ����
    }

    public override void OnUpdate(Vector2 input, Transform playerTransform, CharacterController playerController, PlayerMovementStats movementStats)
    {
        // ����
        Vector3 dir = new Vector3(input.x, 0f, input.y);

        if (dir.magnitude > 0)
        {
            // �̵�
            playerController.Move(dir * movementStats.moveSpeed * Time.deltaTime);

            // ȸ��
            Quaternion dirQuat = Quaternion.LookRotation(dir);
            Quaternion nextQuat = Quaternion.Slerp(playerTransform.rotation, dirQuat, movementStats.rotateSpeed * Time.deltaTime);
            playerTransform.rotation = nextQuat;
        }
    }

    public override void OnExit()
    {
        // �ִϸ��̼� �����Լ������� ����
    }
}
