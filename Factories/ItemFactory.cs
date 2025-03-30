// Licensed to the .NET Foundation under one or more agreements.

using Engine.Models;

namespace Engine.Factories;

public static class ItemFactory
{
    private static List<GameItem> s_standardGameItem;

    static ItemFactory()
    {
        s_standardGameItem = [];
        s_standardGameItem.Add(new Weapon(1001, "Pointy Stick", 1, 1, 2));
        s_standardGameItem.Add(new Weapon(1002, "Rusty Sword", 5, 1, 3));
        s_standardGameItem.Add(new GameItem(9001, "Snake fang", 1));
        s_standardGameItem.Add(new GameItem(9001, "Snakeskin", 2));
    }

    public static GameItem CreateGameItem(int itemTypeID)
    {
        var standardItem = s_standardGameItem.FirstOrDefault(item => item.ItemID == itemTypeID);
        if (standardItem != null)
        {
            return standardItem.Clone();
        }

        return CreateGameItem(1001);
    }
}
