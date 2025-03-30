// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

public class World
{
    private List<Location> _locations = [];

    internal void AddLocation(int x, int y, Location location)
    {
        location.PlaceLocation(x, y);
        _locations.Add(location);
    }

    public Location? LocationAt(int xCoord, int yCoord)
    {
        try
        {
            var locationAt = _locations.Single(x =>
                x.XCoordinate == xCoord && x.YCoordinate == yCoord
            );
            return locationAt;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
