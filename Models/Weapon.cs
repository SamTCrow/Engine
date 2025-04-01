// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

public class Weapon(int itemTypeId, string name, int price, int minDamage, int maxDamage)
    : GameItem(itemTypeId, name, price, true)
{
    public int MinimumDamage { get; } = minDamage;
    public int MaximumDamage { get; } = maxDamage;

    public new Weapon Clone() => new(ItemID, Name, Price, MinimumDamage, MaximumDamage);
}
