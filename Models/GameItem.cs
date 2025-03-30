// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

public class GameItem
{
    public int ItemID { get; }
    public string Name { get; }
    public int Price { get; }

    public GameItem(int itemTypeId, string name, int price)
    {
        ItemID = itemTypeId;
        Name = name;
        Price = price;
    }

    public GameItem Clone() => new GameItem(ItemID, Name, Price);
}
