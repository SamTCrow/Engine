// Licensed to the .NET Foundation under one or more agreements.

using Engine.Models;

namespace Engine.Factories;

public static class ItemFactory
{
    private static readonly List<GameItem> s_standardGameItem = [];

    static ItemFactory()
    {
        s_standardGameItem.Add(
            new GameItem(ItemCategory.Weapon, 1001, "Pointy Stick", 1, true, 1, 2)
        );
        s_standardGameItem.Add(
            new GameItem(ItemCategory.Weapon, 1002, "Rusty Sword", 5, true, 1, 3)
        );
        s_standardGameItem.Add(new GameItem(ItemCategory.Miscellaneous, 9001, "Snake fang", 1));
        s_standardGameItem.Add(new GameItem(ItemCategory.Miscellaneous, 9002, "Snakeskin", 2));
        s_standardGameItem.Add(new GameItem(ItemCategory.Miscellaneous, 9003, "Rat tail", 1));
        s_standardGameItem.Add(new GameItem(ItemCategory.Miscellaneous, 9004, "Rat fur", 2));
        s_standardGameItem.Add(new GameItem(ItemCategory.Miscellaneous, 9005, "Spider fang", 1));
        s_standardGameItem.Add(new GameItem(ItemCategory.Miscellaneous, 9006, "Spider silk", 2));
    }

    public static GameItem CreateGameItem(int itemTypeID)
    {
        GameItem? standardItem = s_standardGameItem.FirstOrDefault(item =>
            item.ItemID == itemTypeID
        );
        if (standardItem != null)
        {
            if (standardItem.Category == ItemCategory.Weapon)
                return standardItem.Clone();
            return standardItem.Clone();
        }

        return CreateGameItem(1001);
    }
}
