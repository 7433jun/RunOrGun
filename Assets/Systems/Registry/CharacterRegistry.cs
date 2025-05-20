using System;
using System.Collections.Generic;

public static class CharacterRegistry
{
    private static Player player;
    private static List<Enemy> enemies = new();

    public static Player Player => player;
    public static List<Enemy> Enemies => enemies;

    public static void Register<T>(T entity)
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

    public static void Unregister<T>(T entity)
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

    public static void Clear()
    {
        player = null;
        enemies.Clear();
    }
}
