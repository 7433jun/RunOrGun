using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    public CharacterController Controller { get; private set; }
    public Vector2 MoveInput { get; private set; }

    public StateMachine StateMachine { get; private set; }
    public IROGState PlayerIdleState { get; private set; }
    public IROGState PlayerMoveState { get; private set; }

    private float skinWidthOffset = 0.001f;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        Controller.skinWidth = skinWidthOffset;

        StateMachine = new StateMachine();
        PlayerIdleState = new PlayerIdleState(this);
        PlayerMoveState = new PlayerMoveState(this);
        StateMachine.ChangeState(PlayerIdleState);

        CharacterRegistry.Instance?.Register(this);
    }

    void Update()
    {
        StateMachine.Update();
    }

    void OnDisable()
    {
        CharacterRegistry.Instance?.Unregister(this);
    }

    void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
}
