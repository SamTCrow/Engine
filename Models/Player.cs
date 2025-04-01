// Licensed to the .NET Foundation under one or more agreements.

using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Engine.Models;

public class Player : LivingEntity
{
    private int _experiencePoints;
    public Job CharacterClass { get; }

    public Race CharacterRace { get; }
    public Attributes CharacterAttributes { get; } = new();

    public ObservableCollection<QuestStatus> Quests { get; } = [];

    public int ResourcePoints { get; }

    public int ExperiencePoints
    {
        get => _experiencePoints;
        private set => SetProperty(ref _experiencePoints, value);
    }

    public int Level { get; } = 1;

    public Player(string name)
        : base(name)
    {
        CharacterClass = Job.Fighter;
        MaximumHitPoints = 10;
        CurrentHitPoints = 10;
    }

    public void AddQuest(QuestStatus quest)
    {
        Quests.Add(quest);
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    public void RemoveGold(int amount)
    {
        Gold -= amount;
    }

    public void AddXp(int amount)
    {
        ExperiencePoints += amount;
    }

    public void SetHp(int amount)
    {
        CurrentHitPoints = amount;
    }

    public bool HasAllTheseItems(List<ItemQuantity> items)
    {
        foreach (var item in items)
        {
            if (Inventory.Count(i => i.ItemID == item.ItemId) < item.Quantity)
                return false;
        }
        return true;
    }
}

public class Attributes
{
    public int Body { get; }
    public int Mind { get; }
    public int Wits { get; }
    public int Reflex { get; }
}

public enum Job
{
    Fighter,
    Gunner,
    Scavenger,
    Technician,
    Psyker,
}

public enum Race
{
    Human,
    Android,
    Cyborg,
    Mutant,
    Xeno,
}
