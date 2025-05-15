using UnityEngine;

public class PlaySceneCamera : MonoBehaviour
{
    private Transform target;
    private Vector3 camForward;

    public float camRotationX = 80f;
    public float camHight = 5.5f;

    void Start()
    {
        target = CharacterRegistry.Instance?.Player.transform;

        camForward = Quaternion.Euler(camRotationX, 0f, 0f) * Vector3.forward;
        transform.rotation = Quaternion.Euler(camRotationX, 0f, 0f);
    }

    void Update()
    {
        if (target != null)
        {
            float distance = (camHight - target.position.y) / camForward.y;
            Vector3 offset = camForward * distance;

            Vector3 camPos = target.position + offset;
            camPos.x = 0;
            transform.position = camPos;
        }
    }
}
