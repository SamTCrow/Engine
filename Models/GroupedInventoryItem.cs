// Licensed to the .NET Foundation under one or more agreements.

using CommunityToolkit.Mvvm.ComponentModel;

namespace Engine.Models;

public class GroupedInventoryItem(GameItem item, int quantity) : ObservableObject
{
    private GameItem _item = item;
    private int _quantity = quantity;

    public GameItem Item
    {
        get => _item;
        set => SetProperty(ref _item, value);
    }

    public int Quantity
    {
        get => _quantity;
        set => SetProperty(ref _quantity, value);
    }
}
