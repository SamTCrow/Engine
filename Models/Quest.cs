// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

public class Quest(
    int iD,
    string name,
    string description,
    List<ItemQuantity> itemsToComplete,
    int rewardXP,
    int rewardGold,
    List<ItemQuantity> rewardItems
)
{
    public int ID { get; } = iD;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public List<ItemQuantity> ItemsToComplete { get; } = itemsToComplete;
    public int RewardXP { get; } = rewardXP;
    public int RewardGold { get; } = rewardGold;
    public List<ItemQuantity> RewardItems { get; } = rewardItems;
}
