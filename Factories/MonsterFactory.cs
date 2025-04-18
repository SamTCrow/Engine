﻿// Licensed to the .NET Foundation under one or more agreements.

using Engine.Models;

namespace Engine.Factories;

public static class MonsterFactory
{
    public static Monster GetMonster(int monsterID)
    {
        switch (monsterID)
        {
            case 1:
                Monster snake = new("Snake", "Snake.png", 4, 5, 1);
                snake.CurrentWeapon = ItemFactory.CreateGameItem(1501);
                snake.AddLoot(9001, 25);
                snake.AddLoot(9002, 75);
                return snake;

            case 2:
                Monster rat = new("Rat", "Rat.png", 5, 5, 1);
                rat.CurrentWeapon = ItemFactory.CreateGameItem(1502);
                rat.AddLoot(9003, 25);
                rat.AddLoot(9004, 75);
                return rat;

            case 3:
                Monster spider = new("Giant Spider", "GiantSpider.png", 10, 10, 3);
                spider.CurrentWeapon = ItemFactory.CreateGameItem(1503);
                spider.AddLoot(9005, 25);
                spider.AddLoot(9006, 75);
                return spider;

            default:
                throw new ArgumentException($"MonsterType {monsterID} does not exist");
        }
    }
}
