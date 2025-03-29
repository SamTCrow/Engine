// Licensed to the .NET Foundation under one or more agreements.

using System.ComponentModel;

namespace Engine.Models;

public class Player : INotifyPropertyChanged
{
    public string Name { get; private set; }

    public Job CharacterClass { get; private set; }

    public Race CharacterRace { get; }
    public Attributes CharacterAttributes { get; } = new();

    public int Hitpoints { get; private set; }

    public int ResourcePoints { get; }
    public int Luck { get; }
    public int ExperiencePoints { get; private set; }
    public int Level { get; } = 1;
    public int Gold { get; private set; }

    public Player(string name)
    {
        Name = name;
        CharacterClass = Job.Fighter;
        Hitpoints = 10;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        OnPropertyChanged(nameof(Gold));
    }

    public void AddXp(int amount)
    {
        ExperiencePoints += amount;
        OnPropertyChanged(nameof(ExperiencePoints));
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
