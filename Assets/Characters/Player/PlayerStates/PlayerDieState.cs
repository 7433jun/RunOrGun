using UnityEngine;

public class PlayerDieState : IROGState
{
    private Player player;

    public PlayerDieState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        // �׾����� ���ȭ�� ȣ���Ұ� ������
        player.gameObject.SetActive(false);
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}
