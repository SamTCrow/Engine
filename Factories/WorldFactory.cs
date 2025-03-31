using Engine.Models;

namespace Engine.Factories;

internal static class WorldFactory
{
    internal static World CreateWorld()
    {
        World newWorld = new();
        newWorld.AddLocation(0, -1, new Location("Home", "This is your house.", "PlayerHome.jpeg"));
        newWorld.AddLocation(
            -1,
            -1,
            new Location(
                "Farmer's House",
                "This is the house of your neighbor, Dudley.",
                "TheFarm.jpeg"
            )
        );
        newWorld.AddLocation(
            -2,
            -1,
            new Location(
                "Farmer's Fields",
                "Nothing good is growing here, and there are a lots of places to hide.",
                "TheFields.jpeg"
            )
        );
        newWorld.LocationAt(-2, -1)?.AddMonster(2, 100);

        newWorld.AddLocation(
            -1,
            0,
            new Location("Trading Shop", "The shop of Susan, the trader.", "Trader.png")
        );
        newWorld.AddLocation(
            0,
            0,
            new Location("Town square", "You see a fountain here.", "TownSquare.png")
        );
        newWorld.AddLocation(
            1,
            0,
            new Location(
                "Town Gate",
                "There is a gate here, protecting the town from giant spiders.",
                "TownGate.png"
            )
        );
        newWorld.AddLocation(
            2,
            0,
            new Location(
                "Spider Forest",
                "The trees in this forest are covered with spider webs.",
                "SpiderForest.png"
            )
        );
        newWorld.LocationAt(2, 0)?.AddMonster(3, 100);
        newWorld.AddLocation(
            0,
            1,
            new Location(
                "Herbalist's hut",
                "You see a small hut, with plants drying from the roof.",
                "HerbalistsHut.png"
            )
        );
        newWorld.LocationAt(0, 1)?.AddQuest(QuestFactory.GetQuestByID(1));
        newWorld.AddLocation(
            0,
            2,
            new Location(
                "Herbalist's garden",
                "There are many plants here, with snakes hiding behind them.",
                "HerbalistsGarden.png"
            )
        );
        newWorld.LocationAt(0, 2)?.AddMonster(1, 100);
        return newWorld;
    }
}
