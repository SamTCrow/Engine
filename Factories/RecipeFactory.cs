// Licensed to the .NET Foundation under one or more agreements.

using Engine.Models;

namespace Engine.Factories;

public static class RecipeFactory
{
    private static readonly List<Recipe> s_recipes = [];

    static RecipeFactory()
    {
        Recipe granolaBar = new(1, "Granola bar");
        granolaBar.AddIngredient(3001, 1);
        granolaBar.AddIngredient(3002, 1);
        granolaBar.AddIngredient(3003, 1);
        granolaBar.AddOutputItem(2001, 1);

        s_recipes.Add(granolaBar);
    }

    public static Recipe RecipeByID(int id)
    {
        return s_recipes.First(x => x.ID == id);
    }
}
