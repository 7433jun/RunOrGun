using UnityEngine;

public class PlayerMoveStatSystem
{
    private PlayerMoveStats move;

    public PlayerMoveStatSystem(PlayerMoveStats move, PlayerMoveStatsDTO dto)
    {
        this.move = move;
        InitMove(dto);
    }

    private void InitMove(PlayerMoveStatsDTO dto)
    {

    }
}
