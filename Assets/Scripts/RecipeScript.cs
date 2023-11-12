using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class RecipeScript : MonoBehaviour
{
    private List<Recipe> Recipes = new();

    public TextMeshProUGUI RecipeNameText;
    public TextMeshProUGUI RecipeIngredientsText;
    public TextMeshProUGUI RecipePointsText;

    // Start is called before the first frame update
    void Start()
    {
        Recipes.Add(RecipesDefinition.Burger);

        var recipe = GetRandomRecipe();
        RecipeNameText.text = recipe.Name;
        RecipeIngredientsText.text = GetFormattedIngredients(recipe.Ingredients);
        RecipePointsText.text = recipe.Points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string GetFormattedIngredients(List<Ingredient> ingredients)
    {
        // Format the ingredients list with each ingredient on a new line

        return string.Join("\n", ingredients.Select(ingredient => ingredient.ToString()));
    }

    private Recipe GetRandomRecipe()
    {
        int randomIndex = Random.Range(0, Recipes.Count);
        return Recipes[randomIndex];
    }

    private class Recipe
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public int Points { get; set; }
    }

    private class Ingredient
    {
        public string Name { get; set; }

        public int Ammount { get; set; }

        public override string ToString()
        {
            return $"{Ammount}x {Name}";
        }
    }

    private static class RecipesDefinition
    {
        public static readonly Recipe Burger = new()
        {
            Name = "Burger",
            Description = "This is a Recipe description!",
            Ingredients = new()
            {
                new Ingredient()
                {
                    Name = "Bread",
                    Ammount = 2
                },
                new Ingredient()
                {
                    Name = "Meat",
                    Ammount = 1
                },
                new Ingredient()
                {
                    Name = "Lettuce",
                    Ammount = 1
                },
                new Ingredient()
                {
                    Name = "Tomatos",
                    Ammount = 1
                }
            },
            Points = 10
        };
    }
}
