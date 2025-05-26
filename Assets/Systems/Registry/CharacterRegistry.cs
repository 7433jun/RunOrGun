using System;
using System.Collections.Generic;

public class CharacterRegistry
{
    private static CharacterRegistry instance;
    public static CharacterRegistry Instance
    {
        get
        {
            if (instance == null)
                instance = new CharacterRegistry();
            return instance;
        }
    }

    private Player player;
    private List<Enemy> enemies = new();

    public Player Player => player;
    public List<Enemy> Enemies => enemies;

    public void Register<T>(T entity)
    {
        if (entity is Player p)
        {
            player = p;
        }
        if (entity is Enemy e)
        {
            enemies.Add(e);
        }
    }

    public void Unregister<T>(T entity)
    {
        if (entity is Player player)
        {
            player = null;
        }
        if (entity is Enemy enemy)
        {
            enemies.Remove(enemy);
        }
    }

    public void Clear()
    {
        player = null;
        enemies.Clear();
    }
}
