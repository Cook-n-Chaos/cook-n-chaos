using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipieManager : MonoBehaviour
{
    public List<Ingredient> Ingredients = new();

    public GameObject Recipe;

    // Start is called before the first frame update
    void Start()
    {
        AddRecipe(RecipiesDefinition.Burger);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddRecipe(RecipeValues recipeValues)
    {
        var recipe = Instantiate(Recipe, transform).GetComponent<Recipe>();

        recipe.SetTime(recipeValues.Time);
        recipe.SetLabel(recipeValues.Name);
        recipe.SetPoints(recipeValues.Points);

        var ingredients = recipeValues.IngredientNames.Select(ingredient =>
        {
            return Ingredients.FirstOrDefault(x => x.Name == ingredient)?.Object;
        });
        recipe.SetIngredients(ingredients.ToList());
    }

    private static class RecipiesDefinition
    {
        public static readonly RecipeValues Burger = new()
        {
            Name = "Burger",
            IngredientNames = new()
            {
                "Bun",
                "Patty",
                "Bun"
            },
            Points = 10,
            Time = 10,
        };
    }

    public class RecipeValues
    {
        public string Name { get; set; }

        public List<string> IngredientNames { get; set; }

        public int Points { get; set; }

        public int Time { get; set; }
    }

    [Serializable]
    public class Ingredient
    {
        public string Name;

        public GameObject Object;
    }
}
