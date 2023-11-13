using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class RecipeScript : MonoBehaviour
{
    public static RecipeScript instance;

    private List<Recipe> Recipes = new();

    public TextMeshProUGUI RecipeNameText;
    public TextMeshProUGUI RecipeIngredientsText;
    public TextMeshProUGUI RecipePointsText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        Recipes.Add(RecipesDefinition.Burger);

        var recipe = GetRandomRecipe();
        RecipeNameText.text = recipe.Name;
        RecipeIngredientsText.text = GetFormattedIngredients(recipe.Ingredients);
        RecipePointsText.text = recipe.Points.ToString();
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

    public class Ingredient
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
                    Name = "Tomatoes",
                    Ammount = 1
                }
            },
            Points = 10
        };
    }

    public int CheckIfThereIsRecipeWithTheseIngredients(List<Ingredient> list)
    {
        int score = -1;
        foreach(Recipe recipe in Recipes)
        {
            if(AreIngredientListsEqual(recipe.Ingredients, list))
            {
                score = recipe.Points;
                Recipes.Remove(recipe);
                return score;
            }
        }
        return score;
    }
    private bool AreIngredientListsEqual(List<Ingredient> list1, List<Ingredient> list2)
    {
        IngredientComparer comparer = new IngredientComparer();

        // Check if both lists contain the same ingredients with the same amounts
        bool listsAreEqual = list1.Count == list2.Count && list1.All(i => list2.Contains(i, comparer));

        return listsAreEqual;
    }

    public class IngredientComparer : IEqualityComparer<Ingredient>
    {
        public bool Equals(Ingredient x, Ingredient y)
        {
            return x.Name == y.Name && x.Ammount == y.Ammount;
        }

        public int GetHashCode(Ingredient obj)
        {
            return obj.Name.GetHashCode() ^ obj.Ammount.GetHashCode();
        }
    }
}
