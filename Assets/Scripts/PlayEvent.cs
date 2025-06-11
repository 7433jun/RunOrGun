using System;
using UnityEngine;

public class PlayEvent
{
    public event Action<int> OnPlayerHealed;
    public event Action<int> OnPlayerDamaged;
    public event Action OnPlayerDie;
}
