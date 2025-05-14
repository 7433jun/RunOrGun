using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRegistry : MonoBehaviour
{
    public static CharacterRegistry Instance { get; private set; }
    // player용 enemies용 분리하긴했는데
    // Register 실행될때마다 Invoke되는거 수정해야됨
    // event Action도 잘 모름
    public static event Action OnPlayerRegisteredStatic;
    public static event Action OnEnemyRegisteredStatic;

    private Player player;
    private List<Enemy> enemies = new();

    public Player Player => player;
    public List<Enemy> Enemies => enemies;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Register<T>(T entity)
    {
        if (entity is Player player)
        {
            this.player = player;
            OnPlayerRegisteredStatic?.Invoke();
        }
        if (entity is Enemy enemy)
        {
            enemies.Add(enemy);
            OnEnemyRegisteredStatic?.Invoke();
        }
    }

    public void Unregister<T>(T entity)
    {
        if (entity is Player player)
        {
            this.player = null;
        }
        if (entity is Enemy enemy)
        {
            enemies.Remove(enemy);
        }
    }
}
