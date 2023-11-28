using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredIngriedeints : MonoBehaviour
{
    public List<string> ingredients = new();

    public void AddIngredient(string name)
    {
        ingredients.Add(name);
    }
}
