// Licensed to the .NET Foundation under one or more agreements.

using Engine.Actions;
using Engine.Models;

namespace Engine.Factories;

public static class ItemFactory
{
    private static readonly List<GameItem> s_standardGameItem = [];

    static ItemFactory()
    {
        BuildWeapon(1001, "Pointy Stick", 1, 1, 2);
        BuildWeapon(1002, "Rusty Sword", 5, 1, 3);

        BuildWeapon(1501, "Snake fangs", 0, 0, 2);
        BuildWeapon(1502, "Rat claws", 0, 0, 2);
        BuildWeapon(1503, "Spider fangs", 0, 0, 4);

        BuildHealingItem(2001, "Granola bar", 5, 2);

        BuildMiscellaneousItem(3001, "Oats", 1);
        BuildMiscellaneousItem(3002, "Honey", 2);
        BuildMiscellaneousItem(3003, "Raisins", 2);

        BuildMiscellaneousItem(9001, "Snake fang", 1);
        BuildMiscellaneousItem(9002, "Snakeskin", 2);
        BuildMiscellaneousItem(9003, "Rat tail", 1);
        BuildMiscellaneousItem(9004, "Rat fur", 2);
        BuildMiscellaneousItem(9005, "Spider fang", 1);
        BuildMiscellaneousItem(9006, "Spider silk", 2);
    }

    public static GameItem CreateGameItem(int itemTypeID)
    {
        var standardItem = s_standardGameItem.FirstOrDefault(item => item.ItemID == itemTypeID);
        if (standardItem != null)
        {
            if (standardItem.Category == ItemCategory.Weapon)
                return standardItem.Clone();
            return standardItem.Clone();
        }

        return CreateGameItem(1001);
    }

    private static void BuildMiscellaneousItem(int id, string name, int price)
    {
        s_standardGameItem.Add(new GameItem(ItemCategory.Miscellaneous, id, name, price));
    }

    private static void BuildWeapon(
        int id,
        string name,
        int price,
        int minimumDamage,
        int maximumDamage
    )
    {
        var weapon = new GameItem(ItemCategory.Weapon, id, name, price, true);
        weapon.Action = new AttackWithWeapon(weapon, minimumDamage, maximumDamage);
        s_standardGameItem.Add(weapon);
    }

    private static void BuildHealingItem(int id, string name, int price, int amount)
    {
        GameItem item = new GameItem(ItemCategory.Consumable, id, name, price);
        item.Action = new Heal(item, amount);
        s_standardGameItem.Add(item);
    }

    public static string ItemName(int itemID)
    {
        return s_standardGameItem.FirstOrDefault(x => x.ItemID == itemID)?.Name ?? "";
    }
}
