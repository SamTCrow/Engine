using Engine.Models;

namespace Engine.ViewModels;

public class GameSession
{
    public Player CurrentPlayer { get; }
    public Location CurrentLocation { get; }

    public GameSession()
    {
        var name = "Sam";
        CurrentPlayer = new Player(name);
        CurrentPlayer.AddGold(1000000);
        CurrentLocation = new Location(
            "Home",
            "This is your house",
            "/Engine;component/Images/Locations/PlayerHome.png"
        );
        CurrentLocation.PlaceLocation(0, -1);
    }
}
