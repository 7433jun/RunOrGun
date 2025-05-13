using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    public CharacterController Controller { get; private set; }
    public StateMachine StateMachine { get; private set; }

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        Controller.skinWidth = 0.001f;

        StateMachine = new StateMachine();

        CharacterRegistry.Instance.Register(this);
    }

    void Update()
    {
        
    }

    void OnDestroy()
    {
        CharacterRegistry.Instance.Register(this);
    }
}
