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

    public int Hitpoints { get; private set; }
    public ObservableCollection<GameItem> Inventory { get; private set; }

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
        Inventory = [];
    }

    public void AddItem(GameItem item)
    {
        Inventory.Add(item);
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    public void AddXp(int amount)
    {
        ExperiencePoints += amount;
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
