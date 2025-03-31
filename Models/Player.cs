// Licensed to the .NET Foundation under one or more agreements.

using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Engine.Models;

public class Player : ObservableObject
{
    public string Name { get; private set; }

    public Job CharacterClass { get; private set; }

    public Race CharacterRace { get; }
    public Attributes CharacterAttributes { get; } = new();
    private int _hitpoints;

    public int Hitpoints
    {
        get => _hitpoints;
        private set { SetProperty(ref _hitpoints, value); }
    }

    public ObservableCollection<GameItem> Inventory { get; private set; } = [];
    public ObservableCollection<QuestStatus> Quests { get; private set; } = [];
    public List<GameItem> Weapons => Inventory.Where(i => i is Weapon).ToList();

    public int ResourcePoints { get; }
    public int Luck { get; }

    private int _experiencePoints;

    public int ExperiencePoints
    {
        get => _experiencePoints;
        private set => SetProperty(ref _experiencePoints, value);
    }

    public int Level { get; } = 1;

    private int _gold;

    public int Gold
    {
        get => _gold;
        private set => SetProperty(ref _gold, value);
    }

    public Player(string name)
    {
        Name = name;
        CharacterClass = Job.Fighter;
        Hitpoints = 10;
    }

    public void AddItem(GameItem item)
    {
        Inventory.Add(item);
        OnPropertyChanged(nameof(Weapons));
    }

    public void AddQuest(QuestStatus quest)
    {
        Quests.Add(quest);
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    public void AddXp(int amount)
    {
        ExperiencePoints += amount;
    }

    public void TakeDamage(int amount)
    {
        Hitpoints -= amount;
    }

    public void SetHp(int amount)
    {
        Hitpoints = amount;
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
