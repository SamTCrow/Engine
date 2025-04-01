// Licensed to the .NET Foundation under one or more agreements.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Engine.Models;

public class Trader : LivingEntity
{
    public Trader(string name)
        : base(name)
    {
        Name = name;
    }
}
