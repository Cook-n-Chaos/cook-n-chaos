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

    private bool RecipeChecker(Recipe newRecipe)
    {
        for (int i = 0; i < Recipes.Count; i++)
        {
            if (AreRecipesEqual(Recipes[i], newRecipe))
            {
                // The new recipe is equal to an existing recipe, remove it
                Score.instance.AddScore(Recipes[i].Points);
                Recipes.RemoveAt(i);
                return true;
            }
        }

        // No match found, the new recipe is unique
        return false;
    }

    private bool AreRecipesEqual(Recipe recipe1, Recipe recipe2)
    {
        // Check if the names are the same
        bool namesEqual = recipe1.Name == recipe2.Name;

        // Check if the descriptions are the same
        bool descriptionsEqual = recipe1.Description == recipe2.Description;

        // Check if the ingredient lists are the same
        bool ingredientsEqual = AreIngredientListsEqual(recipe1.Ingredients, recipe2.Ingredients);

        // Check if the points are the same
        bool pointsEqual = recipe1.Points == recipe2.Points;

        // The recipes are equal if all the above conditions are true
        return namesEqual && descriptionsEqual && ingredientsEqual && pointsEqual;
    }

    private bool AreIngredientListsEqual(List<Ingredient> list1, List<Ingredient> list2)
    {
        // Check if the count is the same
        if (list1.Count != list2.Count)
        {
            return false;
        }

        // Check each ingredient in the list
        for (int i = 0; i < list1.Count; i++)
        {
            // Check if the ingredients are equal
            if (!AreIngredientsEqual(list1[i], list2[i]))
            {
                return false;
            }
        }

        // All ingredients are equal
        return true;
    }

    private bool AreIngredientsEqual(Ingredient ingredient1, Ingredient ingredient2)
    {
        // Check if the names and amounts are the same
        return ingredient1.Name == ingredient2.Name && ingredient1.Ammount == ingredient2.Ammount;
    }
}
