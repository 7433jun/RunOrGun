using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    public CharacterController Controller { get; private set; }
    public Vector2 MoveInput { get; private set; }

    [SerializeField]
    Vector2 inputVec2;
    Vector3 moveVec3;

    public StateMachine StateMachine { get; private set; }
    public IROGState PlayerIdleState { get; private set; }
    public IROGState PlayerMoveState { get; private set; }

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        Controller.skinWidth = 0.001f;

        StateMachine = new StateMachine();
        PlayerIdleState = new PlayerIdleState(this);
        PlayerMoveState = new PlayerMoveState(this);
        StateMachine.ChangeState(PlayerIdleState);
    }

    //void FixedUpdate()
    //{
    //    Controller.Move(moveVec3 * speed * Time.deltaTime);
    //}

    void Update()
    {
        //moveVec3 = Vector3.right * inputVec2.x + Vector3.forward * inputVec2.y;
        //moveVec3.y = 0;
        //
        //if (moveVec3.magnitude > 0)
        //{
        //    Quaternion dirQuat = Quaternion.LookRotation(moveVec3);
        //    Quaternion nextQuat = Quaternion.Slerp(transform.rotation, dirQuat, 0.3f);
        //    transform.rotation = nextQuat;
        //}

        StateMachine.Update();
    }

    void OnMove(InputValue value)
    {
        inputVec2 = value.Get<Vector2>();
        MoveInput = value.Get<Vector2>();
    }
}
