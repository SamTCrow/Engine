// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

public class MonsterEncounter(int monsterID, int chanceOfEncountering)
{
    public int MonsterID { get; } = monsterID;
    public int ChanceOfEncountering { get; set; } = chanceOfEncountering;
}
