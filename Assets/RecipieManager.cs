using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RecipieManager : MonoBehaviour
{
    public List<Ingredient> Ingredients = new();

    [SerializeField] private float levelTimer = 25f;
    private float lastTimer;
    private int recipesPerLevel = 5;
    private int spawnedRecipes = 0;

    public GameObject Recipe;

    // Start is called before the first frame update
    void Start()
    {
        lastTimer = levelTimer;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        SpawnRecipe();
    }
    void UpdateTimer()
    {
        levelTimer -= Time.deltaTime;
    }
    void SpawnRecipe()
    {
        if (spawnedRecipes < recipesPerLevel && lastTimer - (lastTimer / recipesPerLevel) * spawnedRecipes >= levelTimer)
        {
            spawnedRecipes++;
            AddRecipe(GetRandomRecipe());
        }
    }
    private RecipeValues GetRandomRecipe()
    {
        int randomIndex = Random.Range(0, Recipes.Count);
        return Recipes[randomIndex];
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

    private List<RecipeValues> Recipes = new()
    {
         new(){
            Name = "Burger",
            IngredientNames = new()
            {
                "Bun",
                "Patty",
                "Bun"
            },
            Points = 10,
            Time = 10,
        },
         new()  {
            Name = "Big Burger",
            IngredientNames = new()
            {
                "Bun",
                "Patty",
                "Tomatoes",
                "Bun"
            },
            Points = 15,
            Time = 15,
        },
          new()  {
            Name = "Full Burger",
            IngredientNames = new()
            {
                "Bun",
                "Patty",
                "Tomatoes",
                "Lettuce",
                "Bun"
            },
            Points = 15,
            Time = 15,
        },
           new()  {
            Name = "Vegan Burger",
            IngredientNames = new()
            {
                "Bun",
                "Carrots",
                "Tomatoes",
                "Lettuce",
                "Bun"
            },
            Points = 15,
            Time = 15,
        }
};          

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
