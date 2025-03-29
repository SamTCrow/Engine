// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

public class Player(string name)
{
    public string Name
    {
        get => name;
    }

    public Job CharacterClass { get; }
    public Race CharacterRace { get; }
    public Attributes CharacterAttributes { get; } = new();
    public int Hitpoints { get; }
    public int Luck { get; }
    public int ExperiencePoint { get; }
    public int Level { get; }
    public int Gold { get; }
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
