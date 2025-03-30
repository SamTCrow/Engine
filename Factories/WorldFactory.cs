using Engine.Models;

namespace Engine.Factories;

internal static class WorldFactory
{
    internal static World CreateWorld()
    {
        World newWorld = new();
        newWorld.AddLocation(
            0,
            -1,
            new Location(
                "Home",
                "This is your house.",
                "pack://application:,,,/Engine;component/Images/Locations/PlayerHome.jpeg"
            )
        );
        newWorld.AddLocation(
            -1,
            -1,
            new Location(
                "Farmer's House",
                "This is the house of your neighbor, Dudley.",
                "pack://application:,,,/Engine;component/Images/Locations/TheFarm.jpeg"
            )
        );
        newWorld.AddLocation(
            -2,
            -1,
            new Location(
                "Farmer's Fields",
                "Nothing good is growing here, and there are a lots of places to hide.",
                "pack://application:,,,/Engine;component/Images/Locations/TheFields.jpeg"
            )
        );
        newWorld.AddLocation(
            -1,
            0,
            new Location(
                "Trading Shop",
                "The shop of Susan, the trader.",
                "/Engine;component/Images/Locations/Trader.png"
            )
        );
        newWorld.AddLocation(
            0,
            0,
            new Location(
                "Town square",
                "You see a fountain here.",
                "/Engine;component/Images/Locations/TownSquare.png"
            )
        );
        newWorld.AddLocation(
            1,
            0,
            new Location(
                "Town Gate",
                "There is a gate here, protecting the town from giant spiders.",
                "/Engine;component/Images/Locations/TownGate.png"
            )
        );
        newWorld.AddLocation(
            2,
            0,
            new Location(
                "Spider Forest",
                "The trees in this forest are covered with spider webs.",
                "/Engine;component/Images/Locations/SpiderForest.png"
            )
        );
        newWorld.AddLocation(
            0,
            1,
            new Location(
                "Herbalist's hut",
                "You see a small hut, with plants drying from the roof.",
                "/Engine;component/Images/Locations/HerbalistsHut.png"
            )
        );
        newWorld.AddLocation(
            0,
            2,
            new Location(
                "Herbalist's garden",
                "There are many plants here, with snakes hiding behind them.",
                "/Engine;component/Images/Locations/HerbalistsGarden.png"
            )
        );
        return newWorld;
    }
}
