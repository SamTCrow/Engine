// Licensed to the .NET Foundation under one or more agreements.

using Engine.Factories;

namespace Engine.Models;

public class Location
{
    public int XCoordinate { get; }
    public int YCoordinate { get; }
    public string Name { get; }
    public string Description { get; }
    public string ImageName { get; }
    public List<Quest> QuestsAvailableHere { get; } = [];
    public List<MonsterEncounter> MonstersHere { get; } = [];
    public Trader? TraderHere { get; private set; }

    public Location(string name, string description, string imageName, int x, int y)
    {
        Name = name;
        Description = description;
        ImageName = $"/Engine;component/Images/Locations/{imageName}";
        XCoordinate = x;
        YCoordinate = y;
    }

    public void AddQuest(Quest quest)
    {
        QuestsAvailableHere.Add(quest);
    }

    public void AddTrader(Trader trader) => TraderHere = trader;

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
