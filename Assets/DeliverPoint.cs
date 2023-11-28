using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if(go.layer == 7)
        {
            StoredIngriedeints storedIngriedeints = go.GetComponent<StoredIngriedeints>();
            if (storedIngriedeints == null)
                return;
            int score = RecipieManager.instance.CheckIfThereIsRecipeWithTheseIngredients(storedIngriedeints.ingredients);
            if (score > 0)
            {
                Debug.Log("Correct Recipe Deliverd");
                Score.instance.AddScore(score);
                Destroy(go);
            }
            else
            {
                Debug.Log("Wrong Recipe Delivered");
            }
                
        }
    }
}
