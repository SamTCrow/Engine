// Licensed to the .NET Foundation under one or more agreements.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Engine.Models;

public abstract class LivingEntity(string name, int maximumHitPoints, int gold, int level = 1)
    : ObservableObject
{
    private string _name = name;
    private int _currentHitPoints = maximumHitPoints;
    private int _maximumHitPoints = maximumHitPoints;
    private int _gold = gold;
    private int _level = level;

    public string Name
    {
        get => _name;
        private set => SetProperty(ref _name, value);
    }

    public int CurrentHitPoints
    {
        get => _currentHitPoints;
        private set => SetProperty(ref _currentHitPoints, value);
    }

    public int MaximumHitPoints
    {
        get => _maximumHitPoints;
        protected set => SetProperty(ref _maximumHitPoints, value);
    }

    public int Gold
    {
        get => _gold;
        private set => SetProperty(ref _gold, value);
    }

    public int Level
    {
        get => _level;
        protected set => SetProperty(ref _level, value);
    }

    public bool IsDead => CurrentHitPoints <= 0;

    public ObservableCollection<GameItem> Inventory { get; } = [];
    public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; } = [];
    public List<GameItem> Weapons => [.. Inventory.Where(x => x.Category == ItemCategory.Weapon)];

    public event EventHandler? OnKilled;

    public void AddItem(GameItem item)
    {
        Inventory.Add(item);
        if (item.IsUnique)
        {
            GroupedInventory.Add(new(item, 1));
        }
        else
        {
            if (!GroupedInventory.Any(x => x.Item.ItemID == item.ItemID))
            {
                GroupedInventory.Add(new(item, 0));
            }
            GroupedInventory.First(x => x.Item.ItemID == item.ItemID).Quantity++;
        }
        OnPropertyChanged(nameof(Weapons));
    }

    public void RemoveItem(GameItem item)
    {
        if (Inventory.Any(x => x.ItemID == item.ItemID))
            Inventory.Remove(item);
        var itemToRemove = item.IsUnique
            ? GroupedInventory.FirstOrDefault(x => x.Item == item)
            : GroupedInventory.FirstOrDefault(x => x.Item.ItemID == item.ItemID);
        if (itemToRemove != null)
        {
            if (itemToRemove.Quantity == 1)
            {
                GroupedInventory.Remove(itemToRemove);
            }
            else
            {
                itemToRemove.Quantity--;
            }
        }
        OnPropertyChanged(nameof(Weapons));
    }

    public void TakeDamage(int amount)
    {
        CurrentHitPoints -= amount;
        if (IsDead)
        {
            CurrentHitPoints = 0;
            RaiseOnKilledEvent();
        }
    }

    public void Heal(int amount)
    {
        CurrentHitPoints += amount;
        if (CurrentHitPoints > MaximumHitPoints)
        {
            CurrentHitPoints = MaximumHitPoints;
        }
    }

    public void CompletelyHeal()
    {
        CurrentHitPoints = MaximumHitPoints;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    public void RemoveGold(int amount)
    {
        if (amount > Gold)
        {
            throw new ArgumentOutOfRangeException($"{Name} don't have enough gold.");
        }
        Gold -= amount;
    }

    private void RaiseOnKilledEvent()
    {
        OnKilled?.Invoke(this, System.EventArgs.Empty);
    }
}
