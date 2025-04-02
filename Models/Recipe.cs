// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

public class Recipe(int id, string name)
{
    public int ID { get; } = id;
    public string Name { get; } = name;
    public List<ItemQuantity> Ingredients { get; } = [];
    public List<ItemQuantity> OutputItems { get; } = [];

    public void AddIngredient(int itemId, int quantity)
    {
        if (!Ingredients.Any(x => x.ItemId == itemId))
        {
            Ingredients.Add(new ItemQuantity(itemId, quantity));
        }
    }

    public void AddOutputItem(int itemId, int quantity)
    {
        if (!OutputItems.Any(x => x.ItemId == itemId))
        {
            OutputItems.Add(new ItemQuantity(itemId, quantity));
        }
    }
}
