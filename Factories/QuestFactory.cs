// Licensed to the .NET Foundation under one or more agreements.

using Engine.Models;

namespace Engine.Factories;

internal static class QuestFactory
{
    private static readonly List<Quest> s_quests = [];

    static QuestFactory()
    {
        List<ItemQuantity> itemsToComplete = [];
        List<ItemQuantity> rewardItems = [];
        itemsToComplete.Add(new ItemQuantity(9001, 5));
        rewardItems.Add(new ItemQuantity(1002, 1));
        s_quests.Add(
            new Quest(
                1,
                "Clear the herb garden",
                "Defeat the snakes in the Herbalist's garden",
                itemsToComplete,
                25,
                10,
                rewardItems
            )
        );
    }

    internal static Quest GetQuestByID(int id) =>
        s_quests.FirstOrDefault(x => x.ID == id) ?? GetQuestByID(1);
}
