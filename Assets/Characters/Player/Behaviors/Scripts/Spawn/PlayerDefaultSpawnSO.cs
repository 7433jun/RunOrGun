using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDefaultSpawnSO", menuName = "Scriptable Objects/PlayerDefaultSpawnSO")]
public class PlayerDefaultSpawnSO : PlayerSpawnBehaviorSO
{
    private Player player;

    public override void InitBehavior(Player player)
    {
        this.player = player;
    }

    public override void EnterBehavior()
    {
        
    }

    public override void UpdateBehavior()
    {

    }

    public override void ExitBehavior()
    {
        
    }
}