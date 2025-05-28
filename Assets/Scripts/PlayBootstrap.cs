using UnityEngine;

public class PlayBootstrap : MonoBehaviour
{
    [SerializeField] private PlayContext context;
    [SerializeField] private CharacterSpawner characterSpawner;
    [SerializeField] private PlaySceneCamera camera;

    public void Compose()
    {
        // 컨텍스트 초기화
        context.Initialize();

        // 플레이어 생성, 주입
        var player = characterSpawner.SpawnPlayer();
        player.Initialize(context.CharacterRegistry);

        // 적 생성, 주입
        foreach (var enemy in characterSpawner.SpawnEnemy())
        {
            enemy.Initialize(context.CharacterRegistry);
        }


        // 카메라 초기화
        





    }
}
