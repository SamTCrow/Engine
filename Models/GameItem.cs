// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

public class GameItem(
    ItemCategory category,
    int itemTypeId,
    string name,
    int price,
    bool isUnique = false,
    int minimumDamage = 0,
    int maximumDamage = 0
)
{
    public ItemCategory Category { get; } = category;
    public int ItemID { get; } = itemTypeId;
    public string Name { get; } = name;
    public int Price { get; } = price;
    public bool IsUnique { get; } = isUnique;
    public int MinimumDamage { get; } = minimumDamage;
    public int MaximumDamage { get; } = maximumDamage;

    public GameItem Clone() =>
        new(Category, ItemID, Name, Price, IsUnique, MinimumDamage, MaximumDamage);
}

public enum ItemCategory
{
    Miscellaneous,
    Weapon,
}
