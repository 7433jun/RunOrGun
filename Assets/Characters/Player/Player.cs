using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }

    public StateMachine StateMachine { get; private set; }
    public IROGState PlayerIdleState { get; private set; }
    public IROGState PlayerMoveState { get; private set; }
    public IROGState PlayerAttackState { get; private set; }

    public PlayerMovementHandler Movement { get; private set; }
    public PlayerCombatHandler Combat { get; private set; }

    void Awake()
    {
        Movement = GetComponent<PlayerMovementHandler>();
        Combat = GetComponent<PlayerCombatHandler>();
    }

    void OnEnable()
    {
        CharacterRegistry.Instance?.Register(this);
    }

    void Start()
    {
        StateMachine = new StateMachine();
        PlayerIdleState = new PlayerIdleState(this);
        PlayerMoveState = new PlayerMoveState(this);
        PlayerAttackState = new PlayerAttackState(this);
        StateMachine.ChangeState(PlayerIdleState);
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
