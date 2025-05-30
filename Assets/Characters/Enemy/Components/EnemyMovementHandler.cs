using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyMovementHandler : MonoBehaviour
{
    private CharacterController controller;

    private float skinWidthOffset = 0.001f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        controller.skinWidth = skinWidthOffset;
    }

}
