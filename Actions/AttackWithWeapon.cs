// Licensed to the .NET Foundation under one or more agreements.

using Engine.Models;

namespace Engine.Actions;

internal class AttackWithWeapon
{
    private readonly GameItem _weapon;
    private readonly int _maximumDamage;
    private readonly int _minimumDamage;

    public event EventHandler<string>? OnActionPerformed;

    public AttackWithWeapon(GameItem weapon, int minimumDamage, int maximumDamage)
    {
        if (weapon.Category != ItemCategory.Weapon)
            throw new ArgumentException($"{weapon.Name} is not a weapon");

        if (_minimumDamage < 0)
            throw new ArgumentException("minimumDamage must be 0 or larger");

        if (maximumDamage < minimumDamage)
            throw new ArgumentException("maximumDamage must be >= minimumDamage");

        _weapon = weapon;
        _maximumDamage = maximumDamage;
        _minimumDamage = minimumDamage;
    }
}
