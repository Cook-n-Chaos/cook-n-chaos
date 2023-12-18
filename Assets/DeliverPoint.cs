using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverPoint : MonoBehaviour
{
    [SerializeField] ParticleSystem _deliverItemParticles;
    [SerializeField] ParticleSystem _deliverWrongItemParticles;
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
                AudioManager.Instance.Play("correct");
                Instantiate(_deliverItemParticles, go.transform.position, Quaternion.identity);
                RecipieManager.instance.DeliverRecipe();
                Score.instance.AddScore(score);
                Destroy(go);
            }
            else
            {
                Instantiate(_deliverWrongItemParticles, go.transform.position, Quaternion.identity);
                AudioManager.Instance.Play("wrong");
                Debug.Log("Wrong Recipe Delivered");
                Destroy(go);
            }
                
        }
    }
}
