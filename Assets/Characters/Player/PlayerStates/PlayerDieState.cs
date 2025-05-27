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
        // 죽었을때 결과화면 호출할것 같은곳
        player.gameObject.SetActive(false);
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}
