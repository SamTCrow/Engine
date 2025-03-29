// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.Models;

public class Location
{
    public int XCoordinate { get; private set; }
    public int YCoordinate { get; private set; }
    public string Name { get; }
    public string Description { get; }
    public string ImageName { get; }

    public Location(string name, string description, string imageName)
    {
        Name = name;
        Description = description;
        ImageName = imageName;
    }

    public void PlaceLocation(int x, int y)
    {
        XCoordinate = x;
        YCoordinate = y;
    }
}
