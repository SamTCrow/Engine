// Licensed to the .NET Foundation under one or more agreements.

using System.Collections.ObjectModel;

namespace Engine.Models;

public class Player : LivingEntity
{
    private int _classHpModifier = 10;
    private int _experiencePoints;
    public Job CharacterClass { get; }

    public Race CharacterRace { get; }
    public Attributes CharacterAttributes { get; } = new();

    public int ResourcePoints { get; }

    public int ExperiencePoints
    {
        get => _experiencePoints;
        private set
        {
            SetProperty(ref _experiencePoints, value);
            SetLevelAndHP();
        }
    }

    public ObservableCollection<QuestStatus> Quests { get; } = [];

    public event EventHandler? OnLeveledUp;

    public Player(string name, int maximumHitPoints, int gold)
        : base(name, maximumHitPoints, gold)
    {
        CharacterClass = Job.Fighter;
    }

    public void AddQuest(QuestStatus quest)
    {
        Quests.Add(quest);
    }

    public void AddXp(int amount)
    {
        ExperiencePoints += amount;
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

    private void SetLevelAndHP()
    {
        int originalLevel = Level;
        Level = (ExperiencePoints / 100) + 1;
        if (Level != originalLevel)
        {
            MaximumHitPoints = Level * _classHpModifier;
            OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
        }
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
