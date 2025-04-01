// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

public class GameItem(int itemTypeId, string name, int price, bool isUnique = false)
{
    public int ItemID { get; } = itemTypeId;
    public string Name { get; } = name;
    public int Price { get; } = price;
    public bool IsUnique { get; } = isUnique;

    public GameItem Clone() => new(ItemID, Name, Price);
}
