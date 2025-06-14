using System;
using UnityEngine;

public class PlayerStatsSystem : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    public PlayerStats Stats => playerStats;

    public PlayerHealthStatSystem HealthSystem;
    public PlayerMoveStatSystem MoveSystem;
    public PlayerAttackStatSystem AttackSystem;
    public PlayerAmmoStatSystem AmmoSystem;
    public PlayerProjectileStatSystem ProjectileSystem;
    


    public void ApplyStats(PlayerStatsDTO dto)
    {
        playerStats = new PlayerStats();

        HealthSystem = new PlayerHealthStatSystem(playerStats.Health, dto.Health);
    }










    
}
