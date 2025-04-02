// Licensed to the .NET Foundation under one or more agreements.

using Engine.Models;

namespace Engine.Actions;

public interface IAction
{
    event EventHandler<string>? OnActionPerformed;

    void Execute(LivingEntity actor, LivingEntity target);
}
