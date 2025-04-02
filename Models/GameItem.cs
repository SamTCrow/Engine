// Licensed to the .NET Foundation under one or more agreements.

using Engine.Actions;

namespace Engine.Models;

public class GameItem(
    ItemCategory category,
    int itemTypeId,
    string name,
    int price,
    bool isUnique = false,
    IAction? action = null
)
{
    public ItemCategory Category { get; } = category;
    public int ItemID { get; } = itemTypeId;
    public string Name { get; } = name;
    public int Price { get; } = price;
    public bool IsUnique { get; } = isUnique;

    public IAction? Action { get; set; } = action;

    public GameItem Clone() => new(Category, ItemID, Name, Price, IsUnique, Action);

    public void PerformAction(LivingEntity actor, LivingEntity target)
    {
        Action?.Execute(actor, target);
    }
}

public enum ItemCategory
{
    Miscellaneous,
    Weapon,
    Consumable,
}
