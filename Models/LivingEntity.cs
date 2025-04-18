﻿// Licensed to the .NET Foundation under one or more agreements.

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
    private GameItem? _currentWeapon;
    private GameItem? _currentConsumable;

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

    public GameItem? CurrentWeapon
    {
        get => _currentWeapon;
        set
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.Action!.OnActionPerformed -= RaiseActionPerformedEvent;
            }
            SetProperty(ref _currentWeapon, value);
            if (_currentWeapon != null)
            {
                _currentWeapon.Action!.OnActionPerformed += RaiseActionPerformedEvent;
            }
        }
    }

    public GameItem? CurrentConsumable
    {
        get => _currentConsumable;
        set
        {
            if (_currentConsumable != null)
            {
                _currentConsumable.Action!.OnActionPerformed -= RaiseActionPerformedEvent;
            }
            SetProperty(ref _currentConsumable, value);
            if (_currentConsumable != null)
            {
                _currentConsumable.Action!.OnActionPerformed += RaiseActionPerformedEvent;
            }
        }
    }

    public bool IsDead => CurrentHitPoints <= 0;

    public ObservableCollection<GameItem> Inventory { get; } = [];
    public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; } = [];
    public List<GameItem> Weapons => [.. Inventory.Where(x => x.Category == ItemCategory.Weapon)];

    public List<GameItem> Consumables =>
        [.. Inventory.Where(x => x.Category == ItemCategory.Consumable)];

    public bool HasConsumable => Consumables.Count > 0;

    public event EventHandler<string>? OnActionPerformed;

    public event EventHandler? OnKilled;

    public void UseCurrentWeaponOn(LivingEntity target)
    {
        CurrentWeapon?.PerformAction(this, target);
    }

    public void UseCurrentConsumable()
    {
        if (CurrentConsumable != null)
        {
            CurrentConsumable.PerformAction(this, this);
            RemoveItem(CurrentConsumable);
        }
    }

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
        OnPropertyChanged(nameof(Consumables));
        OnPropertyChanged(nameof(HasConsumable));
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
        OnPropertyChanged(nameof(Consumables));
        OnPropertyChanged(nameof(HasConsumable));
    }

    public void RemoveItems(List<ItemQuantity> itemQuantities)
    {
        foreach (var itemQuantity in itemQuantities)
        {
            for (int i = 0; i < itemQuantity.Quantity; i++)
            {
                RemoveItem(Inventory.First(x => x.ItemID == itemQuantity.ItemId));
            }
        }
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

    public bool HasAllTheseItems(List<ItemQuantity> items)
    {
        foreach (var item in items)
        {
            if (Inventory.Count(x => x.ItemID == item.ItemId) < item.Quantity)
            {
                return false;
            }
        }
        return true;
    }

    private void RaiseOnKilledEvent()
    {
        OnKilled?.Invoke(this, System.EventArgs.Empty);
    }

    private void RaiseActionPerformedEvent(object? sender, string result)
    {
        OnActionPerformed?.Invoke(this, result);
    }
}
