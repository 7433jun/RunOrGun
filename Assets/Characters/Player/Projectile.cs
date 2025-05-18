using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;

    public void Initilize(float speed)
    {
        this.speed = speed;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
