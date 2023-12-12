using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Overn : MonoBehaviour
{
    private ChangeToCooked cookingObject;
    public float cookingTime = 5.0f; // Set the cooking time in seconds
    public float burnedTime = 10.0f; // Set the burned time in seconds
    public Slider cookingSlider; // Reference to the UI slider

    [SerializeField] Transform cookingLocation;
    [SerializeField] ParticleSystem cookingParticles;


    private Coroutine cookingCoroutine;

    private void Start()
    {
        if (cookingSlider == null)
        {
            Debug.LogError("Cooking Slider is not assigned!");
        }
        else
        {
            cookingSlider.value = 0;
        }
    }
    public void ObjectLeft()
    {
        StopCoroutine(cookingCoroutine); // Stop the cooking coroutine
        cookingSlider.value = 0; // Reset the slider value

        cookingParticles.gameObject.SetActive(false);

        this.cookingObject = null;
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
            ChangeToCooked temp = go.GetComponent<ChangeToCooked>();
            

            if (temp == null || temp.GetIsBurned())
                return;
            cookingObject = temp;
            cookingParticles.gameObject.SetActive(true);
            go.transform.position = cookingLocation.position;
            go.transform.rotation = Quaternion.identity;
            go.transform.parent = transform;
            cookingObject.SetOven(this);
            
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
            cookingParticles.gameObject.SetActive(false);

            this.cookingObject = null;
        }
    }

    IEnumerator CookObject(ChangeToCooked objToCook, float time)
    {
        float timer = 0;
        while (timer < time)
        {
            timer += Time.deltaTime;
            cookingSlider.value = timer / time;
            yield return null;
        }

        objToCook.ChangeToCookedModel();
        cookingSlider.value = 0;

        // Start the burned timer
        cookingCoroutine = StartCoroutine(BurnObject(objToCook, burnedTime));
    }

    IEnumerator BurnObject(ChangeToCooked objToBurn, float time)
    {
        float timer = 0;
        while (timer < time)
        {
            timer += Time.deltaTime;
            cookingSlider.value = timer / time;
            yield return null;
        }

        objToBurn.ChangeToBurnedModel();
    }
}
