using UnityEngine;

public class PlayBootstrap : MonoBehaviour
{
    [SerializeField] private PlayContext context;
    [SerializeField] private CharacterSpawner characterSpawner;
    [SerializeField] private PlaySceneCamera playCamera;

    public void Compose()
    {
        // 스포너 초기화(딕셔너리 등록)
        characterSpawner.Initialize();

        // 컨텍스트 초기화
        context.Initialize();

        // 플레이어 생성, 등록, 주입
        var player = characterSpawner.SpawnPlayer();
        context.CharacterRegistry.Register(player);
        player.Initialize(context);

        // 적 생성, 등록, 주입
        foreach (var enemy in characterSpawner.SpawnEnemy())
        {
            context.CharacterRegistry.Register(enemy);
            enemy.Initialize(context.CharacterRegistry);
        }

        // 카메라 초기화
        playCamera.Initialize(player.transform);





    }
}
