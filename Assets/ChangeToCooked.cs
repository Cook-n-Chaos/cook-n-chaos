using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToCooked : MonoBehaviour
{
    [SerializeField] private Transform modelPlace;
    private GameObject currentModel;
    [SerializeField] private GameObject modelBefore;
    [SerializeField] private GameObject modelCooked;
    [SerializeField] private GameObject modelBurned;
    private Product product;

    private void Awake()
    {
        product = gameObject.GetComponent<Product>();
        currentModel = modelBefore;
    }
    public void ChangeToCookedModel()
    {
        ChangeModel(modelCooked, "Patty");
    }
    public void ChangeToBurnedModel()
    {
        ChangeModel(modelBurned, "burnedPatty");
    }
    public void ChangeModel(GameObject toChangeToModel, string name)
    {
        Vector3 position = modelBefore.transform.position;
        Quaternion rotation = modelBefore.transform.rotation;

        GameObject newModel = Instantiate(toChangeToModel, position, rotation, modelPlace);       
        ChangeProductName(name);

        Destroy(currentModel);
        currentModel = newModel;
    }

    public void ChangeLayer()
    {
        gameObject.layer = 0;
    }

    private void ChangeProductName(string name)
    {
        product.name = name;
    }
}
