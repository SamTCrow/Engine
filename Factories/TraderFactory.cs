// Licensed to the .NET Foundation under one or more agreements.

using Engine.Models;

namespace Engine.Factories;

public static class TraderFactory
{
    private static readonly List<Trader> s_traders = [];

    static TraderFactory()
    {
        Trader susan = new Trader("Susan");
        susan.AddItem(ItemFactory.CreateGameItem(1001));

        var farmerTed = new Trader("Farmer Ted");
        farmerTed.AddItem(ItemFactory.CreateGameItem(1001));

        var peteTheHerbalist = new Trader("Pete the Herbalist");
        peteTheHerbalist.AddItem(ItemFactory.CreateGameItem(1001));

        AddTraderToList(susan);
        AddTraderToList(farmerTed);
        AddTraderToList(peteTheHerbalist);
    }

    private static void AddTraderToList(Trader trader)
    {
        if (s_traders.Any(t => t.Name == trader.Name))
            throw new ArgumentException($"There is already a trader named '{trader.Name}");
        s_traders.Add(trader);
    }

    public static Trader? GetTraderByName(string name) =>
        s_traders.FirstOrDefault(t => t.Name == name);
}
