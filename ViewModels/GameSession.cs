using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Engine.Factories;
using Engine.Models;

namespace Engine.ViewModels;

public class GameSession : ObservableObject
{
    public Player CurrentPlayer { get; }

    private Location _currentLocation;

    public Location CurrentLocation
    {
        get => _currentLocation;
        private set
        {
            SetProperty(ref _currentLocation, value);
            OnPropertyChanged(nameof(ExitNorth));
            OnPropertyChanged(nameof(ExitSouth));
            OnPropertyChanged(nameof(ExitWest));
            OnPropertyChanged(nameof(ExitEast));
        }
    }

    public World CurrentWorld { get; }

    public bool ExitNorth
    {
        get =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1)
            != null;
    }

    public bool ExitSouth
    {
        get =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1)
            != null;
    }

    public bool ExitWest
    {
        get =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate)
            != null;
    }

    public bool ExitEast
    {
        get =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate)
            != null;
    }

    public GameSession()
    {
        var name = "Sam";
        CurrentPlayer = new Player(name);
        CurrentPlayer.AddGold(1000000);
        CurrentWorld = WorldFactory.CreateWorld();
        _currentLocation = CurrentWorld.LocationAt(0, 0)!;
    }

    public void Move(Direction direction)
    {
        Location? newLocation = direction switch
        {
            Direction.North => CurrentWorld.LocationAt(
                CurrentLocation.XCoordinate,
                CurrentLocation.YCoordinate + 1
            ),
            Direction.South => CurrentWorld.LocationAt(
                CurrentLocation.XCoordinate,
                CurrentLocation.YCoordinate - 1
            ),
            Direction.East => CurrentWorld.LocationAt(
                CurrentLocation.XCoordinate + 1,
                CurrentLocation.YCoordinate
            ),
            Direction.West => CurrentWorld.LocationAt(
                CurrentLocation.XCoordinate - 1,
                CurrentLocation.YCoordinate
            ),
            _ => null,
        };
        if (newLocation != null)
        {
            CurrentLocation = newLocation;
        }
    }
}

public enum Direction
{
    North,
    South,
    East,
    West,
}
