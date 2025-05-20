using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    public CharacterController Controller { get; private set; }
    public StateMachine StateMachine { get; private set; }

    void OnEnable()
    {
        CharacterRegistry.Register(this);
    }

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        Controller.skinWidth = 0.001f;

        StateMachine = new StateMachine();
    }

    void Update()
    {
        
    }

    void OnDisable()
    {
        CharacterRegistry.Unregister(this);
    }
}
