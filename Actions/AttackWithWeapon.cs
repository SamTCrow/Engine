// Licensed to the .NET Foundation under one or more agreements.

using Engine.Models;

namespace Engine.Actions;

public class AttackWithWeapon : BaseAction, IAction
{
    private readonly GameItem _weapon;
    private readonly int _maximumDamage;
    private readonly int _minimumDamage;

    public AttackWithWeapon(GameItem weapon, int minimumDamage, int maximumDamage)
        : base(weapon)
    {
        if (weapon.Category != ItemCategory.Weapon)
            throw new ArgumentException($"{weapon.Name} is not a weapon");

        if (minimumDamage < 0)
            throw new ArgumentException("minimumDamage must be 0 or larger");

        if (maximumDamage < minimumDamage)
            throw new ArgumentException("maximumDamage must be >= minimumDamage");

        _weapon = weapon;
        _maximumDamage = maximumDamage;
        _minimumDamage = minimumDamage;
    }

    public void Execute(LivingEntity actor, LivingEntity target)
    {
        var damage = new Random().Next(_minimumDamage, _maximumDamage + 1);
        string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
        string targetName = (target is Player) ? "you" : $"the {target.Name.ToLower()}";
        if (damage == 0)
        {
            ReportResult($"{actorName} missed {targetName}");
        }
        else
        {
            ReportResult(
                $"{actorName} hit {targetName} for {damage} point{(damage > 1 ? "s" : "")}"
            );
            target.TakeDamage(damage);
        }
    }
}
