using UnityEngine;

public class PlayBootstrap : MonoBehaviour
{
    [SerializeField] private CharacterSpawner characterSpawner;

    public void Compose()
    {
        CharacterRegistry.Instance.Clear();

        var player = characterSpawner.SpawnPlayer();
        CharacterRegistry.Instance.Register(player);

        foreach (var enemy in characterSpawner.SpawnEnemy())
        {
            CharacterRegistry.Instance.Register(enemy);
        }
    }
}
