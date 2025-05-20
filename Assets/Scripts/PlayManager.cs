using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] private PlayBootstrap bootstrap;

    void Awake()
    {
        bootstrap.Compose();
    }
}
