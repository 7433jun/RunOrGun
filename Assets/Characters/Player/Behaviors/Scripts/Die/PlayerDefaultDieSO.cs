using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDefaultDieSO", menuName = "Scriptable Objects/PlayerDefaultDieSO")]
public class PlayerDefaultDieSO : PlayerDieBehaviorSO
{
    private Player player;

    public override void InitBehavior(Player player)
    {
        this.player = player;
    }

    public override void EnterBehavior()
    {
        player.characterRegistry.Unregister(player);
    }

    public override void UpdateBehavior()
    {

    }

    public override void ExitBehavior()
    {
        player.gameObject.SetActive(false);
    }
}
