using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;

    CharacterController controller;

    [SerializeField]
    Vector2 inputVec2;
    Vector3 moveVec3;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        controller.skinWidth = 0.001f;
    }

    void FixedUpdate()
    {
        controller.Move(moveVec3 * speed * Time.deltaTime);
    }

    void Update()
    {
        moveVec3 = Vector3.right * inputVec2.x + Vector3.forward * inputVec2.y;
        moveVec3.y = 0;

        if (moveVec3.magnitude > 0)
        {
            Quaternion dirQuat = Quaternion.LookRotation(moveVec3);
            Quaternion nextQuat = Quaternion.Slerp(transform.rotation, dirQuat, 0.3f);
            transform.rotation = nextQuat;
        }
    }

    void OnMove(InputValue value)
    {
        inputVec2 = value.Get<Vector2>();
    }
}
