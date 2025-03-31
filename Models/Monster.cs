// Licensed to the .NET Foundation under one or more agreements.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Engine.Models;

public class Monster : ObservableObject
{
    private int _hitPoints;

    public int HitPoints
    {
        get => _hitPoints;
        private set { SetProperty(ref _hitPoints, value); }
    }

    public string Name { get; }
    public string ImageName { get; }
    public int MaximumHitPoints { get; }
    public int MinimumDamage { get; private set; }
    public int MaximumDamage { get; private set; }
    public int RewardXp { get; }
    public int RewardGold { get; }
    public ObservableCollection<ItemQuantity> Inventory { get; private set; }

    public Monster(
        string name,
        string imageName,
        int hitPoints,
        int maximumHitPoints,
        int minimumDamage,
        int maximumDamage,
        int rewardXp,
        int rewardGold
    )
    {
        HitPoints = hitPoints;
        Name = name;
        ImageName = $"/Engine;component/Images/Monsters/{imageName}";
        MaximumHitPoints = maximumHitPoints;
        RewardXp = rewardXp;
        RewardGold = rewardGold;
        Inventory = [];
        MinimumDamage = minimumDamage;
        MaximumDamage = maximumDamage;
    }

    public void AddLoot(int itemId, int percentage)
    {
        if (new Random().Next(1, 101) <= percentage)
        {
            Inventory.Add(new ItemQuantity(itemId, 1));
        }
    }

    public void TakeDamage(int amount)
    {
        HitPoints -= amount;
    }
}
