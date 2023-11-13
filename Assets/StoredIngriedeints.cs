using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredIngriedeints : MonoBehaviour
{
    public List<RecipeScript.Ingredient> ingredients = new();

    public void AddIngredient(string name)
    {
        RecipeScript.Ingredient ingredient = new();
        foreach(RecipeScript.Ingredient ing in ingredients)
        {
            if(ing.Name == name)
            {
                ing.Ammount++;
                return;
            }
        }
        ingredient.Name = name;
        ingredient.Ammount = 1;
        ingredients.Add(ingredient);
    }
}
