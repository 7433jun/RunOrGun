using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    private CharacterRegistry characterRegistry;

    public CharacterController Controller { get; private set; }
    public StateMachine StateMachine { get; private set; }

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        Controller.skinWidth = 0.001f;

        StateMachine = new StateMachine();
    }

    void Update()
    {
        
    }

    public void Initialize(CharacterRegistry registry)
    {
        characterRegistry = registry;
    }
}
