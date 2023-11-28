using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RecipieManager : MonoBehaviour
{
    public static RecipieManager instance;
    public List<Ingredient> Ingredients = new();

    [SerializeField] private float levelTimer = 500f;
    private float lastTimer;
    [SerializeField] private int recipesPerLevel = 5;
    private int spawnedRecipes = 0;

    public GameObject Recipe;
    private List<Tuple<RecipeValues, GameObject>> currentRecipesToDeliver = new List<Tuple<RecipeValues, GameObject>>();
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
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
        GameObject recipeObject = Instantiate(Recipe, transform);
        var recipe = recipeObject.GetComponent<Recipe>();

        currentRecipesToDeliver.Add(new Tuple<RecipeValues, GameObject>(recipeValues, recipeObject));

        recipe.recipeValues = recipeValues;
        recipe.SetTime(recipeValues.Time);
        recipe.SetLabel(recipeValues.Name);
        recipe.SetPoints(recipeValues.Points);

        var ingredients = recipeValues.IngredientNames.Select(ingredient =>
        {
            return Ingredients.FirstOrDefault(x => x.Name == ingredient)?.Object;
        });
        recipe.SetIngredients(ingredients.ToList());
    }
    public void RemoveRecipeFromCurrentList(RecipeValues values)
    {
        var tupleToRemove = currentRecipesToDeliver.FirstOrDefault(tuple => tuple.Item1.Equals(values));

        if (tupleToRemove != null)
        {
            currentRecipesToDeliver.Remove(tupleToRemove);
        }
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
            Time = 130,
        },
         new(){
            Name = "Meater",
            IngredientNames = new()
            {
               "Patty"
            },
            Points = 10,
            Time = 130,
        },
          new(){
            Name = "Bun Lover",
            IngredientNames = new()
            {
                "Bun",
                "Bun"
            },
            Points = 10,
            Time = 130,
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
            Time = 200,
        },
         new()  {
            Name = "Green Burger",
            IngredientNames = new()
            {
                "Bun",
                "Patty",
                "Lettuce",
                "Bun"
            },
            Points = 15,
            Time = 150,
        }/*,
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
            Time = 150,
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
            Time = 180,
        }*/
};          

    public  class RecipeValues
    {
        public string Name { get; set; }

        public List<string> IngredientNames { get; set; }

        public int Points { get; set; }

        public int Time { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            RecipeValues other = (RecipeValues)obj;

            bool areEqual = true;
            for(int i = 0; i < other.IngredientNames.Count; i++)
            {
                Debug.Log(IngredientNames[i] + "   " + other.IngredientNames[i]);
                if (IngredientNames[i] != other.IngredientNames[i])
                {
                    return false;
                }
            }
            return areEqual; 
        }
    }

    [Serializable]
    public class Ingredient
    {
        public string Name;

        public GameObject Object;
    }

    public int CheckIfThereIsRecipeWithTheseIngredients(List<string> list)
    {
        int score = -1;

        RecipeValues newValues = new()
        {
            IngredientNames = list
        };

        var tupleToRemove = currentRecipesToDeliver.FirstOrDefault(tuple => tuple.Item1.Equals(newValues));

        if (tupleToRemove != null)
        {
            score = tupleToRemove.Item1.Points;
            Destroy(tupleToRemove.Item2);
            currentRecipesToDeliver.Remove(tupleToRemove);
        }
        return score;
    }
}
