// Licensed to the .NET Foundation under one or more agreements.

using CommunityToolkit.Mvvm.ComponentModel;

namespace Engine.Models;

public class QuestStatus(Quest quest) : ObservableObject
{
    private bool _isComplete = false;
    public Quest PlayerQuest { get; } = quest;

    public bool IsComplete
    {
        get => _isComplete;
        private set => SetProperty(ref _isComplete, value);
    }

    public void Complete()
    {
        IsComplete = true;
    }
}
