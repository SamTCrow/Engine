// Licensed to the .NET Foundation under one or more agreements.

using Engine.Models;

namespace Engine.Actions;

internal class Heal : BaseAction, IAction
{
    private readonly GameItem _item;
    private readonly int _healAmount;

    public Heal(GameItem item, int healAmount)
        : base(item)
    {
        if (item.Category != ItemCategory.Consumable)
            throw new ArgumentException($"{item.Name} is not a consumable");

        _item = item;
        _healAmount = healAmount;
    }

    public void Execute(LivingEntity actor, LivingEntity target)
    {
        string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
        string targetName = (target is Player) ? "yourself" : $"the {target.Name.ToLower()}";

        ReportResult(
            $"{actorName} heal {targetName} for {_healAmount} point{(_healAmount > 1 ? "s" : "")}"
        );
        target.Heal(_healAmount);
    }
}
