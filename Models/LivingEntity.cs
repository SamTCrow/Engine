// Licensed to the .NET Foundation under one or more agreements.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Engine.Models;

public abstract class LivingEntity(string name) : ObservableObject
{
    private string _name = name;
    private int _currentHitPoints;
    private int _maximumHitPoints;
    private int _gold;

    public string Name
    {
        get => _name;
        protected set => SetProperty(ref _name, value);
    }

    public int CurrentHitPoints
    {
        get => _currentHitPoints;
        protected set => SetProperty(ref _currentHitPoints, value);
    }

    public int MaximumHitPoints
    {
        get => _maximumHitPoints;
        protected set => SetProperty(ref _maximumHitPoints, value);
    }

    public int Gold
    {
        get => _gold;
        protected set => SetProperty(ref _gold, value);
    }

    public ObservableCollection<GameItem> Inventory { get; set; } = [];
    public List<GameItem> Weapons => [.. Inventory.Where(x => x is Weapon)];

    public void AddItem(GameItem item)
    {
        Inventory.Add(item);
        OnPropertyChanged(nameof(Weapons));
    }

    public void RemoveItem(GameItem item)
    {
        if (Inventory.Any(x => x.ItemID == item.ItemID))
            Inventory.Remove(item);
    }

    public void TakeDamage(int amount)
    {
        CurrentHitPoints -= amount;
    }
}
