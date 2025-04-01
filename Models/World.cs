// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

public class World
{
    private List<Location> _locations = [];

    internal void AddLocation(Location location)
    {
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
