// Licensed to the .NET Foundation under one or more agreements.

using Engine.Factories;

namespace Engine.Models;

public class Location
{
    public int XCoordinate { get; private set; }
    public int YCoordinate { get; private set; }
    public string Name { get; }
    public string Description { get; }
    public string ImageName { get; }
    public List<Quest> QuestsAvailableHere { get; private set; } = [];
    public List<MonsterEncounter> MonstersHere { get; private set; } = [];

    public Location(string name, string description, string imageName)
    {
        Name = name;
        Description = description;
        ImageName = $"/Engine;component/Images/Locations/{imageName}";
    }

    public void PlaceLocation(int x, int y)
    {
        XCoordinate = x;
        YCoordinate = y;
    }

    public void AddQuest(Quest quest)
    {
        QuestsAvailableHere.Add(quest);
    }

    public void AddMonster(int monsterID, int chance)
    {
        if (MonstersHere.Exists(m => m.MonsterID == monsterID))
        {
            MonstersHere.First(m => m.MonsterID == monsterID).ChanceOfEncountering = chance;
        }
        else
        {
            MonstersHere.Add(new MonsterEncounter(monsterID, chance));
        }
    }

    public Monster? GetMonster()
    {
        if (MonstersHere.Count == 0)
        {
            return null;
        }
        int totalChances = MonstersHere.Sum(m => m.ChanceOfEncountering);
        int random = new Random().Next(1, totalChances + 1);
        int runningTotal = 0;
        foreach (MonsterEncounter monsterEncounter in MonstersHere)
        {
            runningTotal += monsterEncounter.ChanceOfEncountering;
            if (random <= runningTotal)
            {
                return MonsterFactory.GetMonster(monsterEncounter.MonsterID);
            }
        }
        return MonsterFactory.GetMonster(MonstersHere.Last().MonsterID);
    }
}
