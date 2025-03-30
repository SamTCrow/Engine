// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

public class Weapon : GameItem
{
    public int MinimumDamage { get; }
    public int MaximumDamage { get; }

    public Weapon(int itemTypeId, string name, int price, int minDamage, int maxDamage)
        : base(itemTypeId, name, price)
    {
        MinimumDamage = minDamage;
        MaximumDamage = maxDamage;
    }

    public new Weapon Clone() => new Weapon(ItemID, Name, Price, MinimumDamage, MaximumDamage);
}
