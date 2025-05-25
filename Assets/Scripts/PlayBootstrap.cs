using UnityEngine;

public class PlayBootstrap : MonoBehaviour
{
    [SerializeField] private CharacterSpawner characterSpawner;

    public void Compose()
    {
        CharacterRegistry.Clear();

        characterSpawner.SpawnPlayer();
        characterSpawner.SpawnEnemy();

    }
}
