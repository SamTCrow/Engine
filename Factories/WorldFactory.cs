using Engine.Models;

namespace Engine.Factories;

internal static class WorldFactory
{
    internal static World CreateWorld()
    {
        World newWorld = new();
        newWorld.AddLocation(new Location("Home", "This is your house.", "PlayerHome.jpeg", 0, -1));
        newWorld.AddLocation(
            new Location(
                "Farmer's House",
                "This is the house of your neighbor, Dudley.",
                "TheFarm.jpeg",
                -1,
                -1
            )
        );
        newWorld.AddLocation(
            new Location(
                "Farmer's Fields",
                "Nothing good is growing here, and there are a lots of places to hide.",
                "TheFields.jpeg",
                -2,
                -1
            )
        );
        newWorld.LocationAt(-2, -1)?.AddMonster(2, 100);
        newWorld.LocationAt(-1, -1)?.AddTrader(TraderFactory.GetTraderByName("Farmer Ted")!);

        newWorld.AddLocation(
            new Location("Trading Shop", "The shop of Susan, the trader.", "Trader.png", -1, 0)
        );
        newWorld.LocationAt(-1, 0)?.AddTrader(TraderFactory.GetTraderByName("Susan")!);
        newWorld.AddLocation(
            new Location("Town square", "You see a fountain here.", "TownSquare.png", 0, 0)
        );
        newWorld.AddLocation(
            new Location(
                "Town Gate",
                "There is a gate here, protecting the town from giant spiders.",
                "TownGate.png",
                1,
                0
            )
        );
        newWorld.AddLocation(
            new Location(
                "Spider Forest",
                "The trees in this forest are covered with spider webs.",
                "SpiderForest.png",
                2,
                0
            )
        );
        newWorld.LocationAt(2, 0)?.AddMonster(3, 100);
        newWorld.AddLocation(
            new Location(
                "Herbalist's hut",
                "You see a small hut, with plants drying from the roof.",
                "HerbalistsHut.png",
                0,
                1
            )
        );
        newWorld.LocationAt(0, 1)?.AddQuest(QuestFactory.GetQuestByID(1));
        newWorld.LocationAt(0, 1)?.AddTrader(TraderFactory.GetTraderByName("Pete the Herbalist")!);
        newWorld.AddLocation(
            new Location(
                "Herbalist's garden",
                "There are many plants here, with snakes hiding behind them.",
                "HerbalistsGarden.png",
                0,
                2
            )
        );
        newWorld.LocationAt(0, 2)?.AddMonster(1, 100);
        return newWorld;
    }
}
