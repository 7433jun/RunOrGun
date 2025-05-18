using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    private CharacterController controller;
    private Transform body;
    private PlayerStats stats;

    private float skinWidthOffset = 0.001f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        body = transform;

        controller.skinWidth = skinWidthOffset;
    }

    public void Initialize(PlayerStats stats)
    {
        this.stats = stats;
    }

    public void Move(Vector2 input)
    {
        Vector3 dir = new Vector3(input.x, 0f, input.y);
        if (dir.magnitude > 0)
        {
            controller.Move(dir * stats.Movement.moveSpeed * Time.deltaTime);
            RotateTowards(dir);
        }
    }

    public void RotateTowards(Vector3 direction)
    {
        direction.y = 0;
        if (direction == Vector3.zero) return;
        Quaternion dirQuat = Quaternion.LookRotation(direction);
        Quaternion nextQuat = Quaternion.Slerp(body.rotation, dirQuat, stats.Movement.rotateSpeed * Time.deltaTime);
        body.rotation = nextQuat;
    }
}
