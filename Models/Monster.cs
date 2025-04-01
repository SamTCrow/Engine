// Licensed to the .NET Foundation under one or more agreements.

using Engine.Factories;

namespace Engine.Models;

public class Monster : LivingEntity
{
    public string ImageName { get; }

    public int MinimumDamage { get; }
    public int MaximumDamage { get; }
    public int RewardXp { get; }

    public Monster(
        string name,
        string imageName,
        int maximumHitPoints,
        int minimumDamage,
        int maximumDamage,
        int rewardXp,
        int rewardGold
    )
        : base(name, maximumHitPoints, rewardGold)
    {
        ImageName = $"/Engine;component/Images/Monsters/{imageName}";
        RewardXp = rewardXp;
        MinimumDamage = minimumDamage;
        MaximumDamage = maximumDamage;
    }

    public void AddLoot(int itemId, int percentage)
    {
        if (new Random().Next(1, 101) <= percentage)
        {
            Inventory.Add(ItemFactory.CreateGameItem(itemId));
        }
    }
}
