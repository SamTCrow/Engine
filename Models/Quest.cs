// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

internal class Quest
{
    public Quest(
        int iD,
        string name,
        string description,
        List<ItemQuantity> itemsToComplete,
        int rewardXP,
        int rewardGold,
        List<ItemQuantity> rewardItems
    )
    {
        ID = iD;
        Name = name;
        Description = description;
        ItemsToComplete = itemsToComplete;
        RewardXP = rewardXP;
        RewardGold = rewardGold;
        RewardItems = rewardItems;
    }

    public int ID { get; }
    public string Name { get; }
    public string Description { get; }
    public List<ItemQuantity> ItemsToComplete { get; }
    public int RewardXP { get; }
    public int RewardGold { get; }
    public List<ItemQuantity> RewardItems { get; }
}
