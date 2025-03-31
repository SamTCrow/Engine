// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

public class QuestStatus(Quest quest)
{
    public Quest PlayerQuest { get; } = quest;
    public bool IsComplete { get; private set; } = false;

    public void Complete()
    {
        IsComplete = true;
    }
}
