using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    private CharacterController controller;
    private Transform body;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    private float skinWidthOffset = 0.001f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        body = transform;

        controller.skinWidth = skinWidthOffset;
    }

    public void Move(Vector2 input)
    {
        Vector3 dir = new Vector3(input.x, 0f, input.y);
        if (dir.magnitude > 0)
        {
            controller.Move(dir * moveSpeed * Time.deltaTime);
            RotateTowards(dir);
        }
    }

    public void RotateTowards(Vector3 direction)
    {
        direction.y = 0;
        if (direction == Vector3.zero) return;
        Quaternion dirQuat = Quaternion.LookRotation(direction);
        Quaternion nextQuat = Quaternion.Slerp(body.rotation, dirQuat, rotateSpeed * Time.deltaTime);
        body.rotation = nextQuat;
    }
}
