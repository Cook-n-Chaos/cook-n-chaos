using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Overn : MonoBehaviour
{
    private ChangeToCooked cookingObject;
    public float cookingTime = 5.0f; // Set the cooking time in seconds
    public float burnedTime = 10.0f; // Set the burned time in seconds
    public Slider cookingSlider; // Reference to the UI slider

    private Coroutine cookingCoroutine;

    private void Start()
    {
        if (cookingSlider == null)
        {
            Debug.LogError("Cooking Slider is not assigned!");
        }
        else
        {
            cookingSlider.maxValue = cookingTime;
            cookingSlider.value = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.layer == 8)
        {
            if (cookingCoroutine != null)
            {
                StopCoroutine(cookingCoroutine); // Stop previous cooking coroutine
                cookingSlider.value = 0; // Reset slider if a new object enters before cooking ends
            }

            cookingObject = go.GetComponent<ChangeToCooked>();
            cookingCoroutine = StartCoroutine(CookObject(cookingObject, cookingTime));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.layer == 8 && cookingObject != null && go == cookingObject.gameObject)
        {
            StopCoroutine(cookingCoroutine); // Stop the cooking coroutine
            cookingSlider.value = 0; // Reset the slider value

            cookingObject.ChangeLayer();

            this.cookingObject = null;
        }
    }

    IEnumerator CookObject(ChangeToCooked objToCook, float time)
    {
        float timer = 0;
        while (timer < time)
        {
            timer += Time.deltaTime;
            cookingSlider.value = timer;
            yield return null;
        }

        objToCook.ChangeToCookedModel();
        cookingSlider.value = 0;

        yield return new WaitForSeconds(1.0f); // Wait for a second after cooking

        // Start the burned timer
        StartCoroutine(BurnObject(objToCook, burnedTime));
    }

    IEnumerator BurnObject(ChangeToCooked objToBurn, float time)
    {
        float timer = 0;
        while (timer < time)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        objToBurn.ChangeToBurnedModel();
    }
}
