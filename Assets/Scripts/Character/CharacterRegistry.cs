using System.Collections.Generic;
using UnityEngine;

public class CharacterRegistry : MonoBehaviour
{
    public static CharacterRegistry Instance { get; private set; }

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
        }
        if (entity is Enemy enemy)
        {
            enemies.Add(enemy);
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
