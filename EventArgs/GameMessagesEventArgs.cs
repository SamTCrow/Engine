// Licensed to the .NET Foundation under one or more agreements.

namespace Engine.EventArgs;

public class GameMessagesEventArgs(string message) : System.EventArgs
{
    public string Mesagge { get; private set; } = message;
}
