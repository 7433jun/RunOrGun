using UnityEngine;

public class PlayBootstrap : MonoBehaviour
{
    [SerializeField] private PlayContext context;
    [SerializeField] private CharacterSpawner characterSpawner;
    [SerializeField] private PlaySceneCamera camera;

    public void Compose()
    {
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
        





    }
}
