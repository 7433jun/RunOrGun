using UnityEngine;

public class PlayBootstrap : MonoBehaviour
{
    [SerializeField] private PlayContext context;
    [SerializeField] private CharacterSpawner characterSpawner;
    [SerializeField] private PlaySceneCamera playCamera;

    public void Compose()
    {
        // ������ �ʱ�ȭ(��ųʸ� ���)
        characterSpawner.Initialize();

        // ���ؽ�Ʈ �ʱ�ȭ
        context.Initialize();

        // �÷��̾� ����, ����
        var player = characterSpawner.SpawnPlayer();
        player.Initialize(context.CharacterRegistry);

        // �� ����, ����
        foreach (var enemy in characterSpawner.SpawnEnemy())
        {
            enemy.Initialize(context.CharacterRegistry);
        }

        // ī�޶� �ʱ�ȭ
        playCamera.Initialize(player.transform);





    }
}
