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
    public bool burned = false;
    private Overn overnIWasCookedIn;

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
        BurnItem();
    }
    public void ChangeModel(GameObject toChangeToModel, string name)
    {
        if (currentModel == null)
            return;
        Vector3 position = currentModel.transform.position;
        Quaternion rotation = currentModel.transform.rotation;

        GameObject newModel = Instantiate(toChangeToModel, position, rotation, modelPlace);       
        ChangeProductName(name);

        Destroy(currentModel);
        currentModel = newModel;
    }

    public void ChangeLayer()
    {
        Debug.Log("change to default layer");
        RemoveOven();
        gameObject.layer = 0;
    }
    public void ChangeLayerToCookable()
    {
        Debug.Log("change to cookable layer");
        gameObject.layer = 8;
    }
    public void SetOven(Overn oven)
    {
        overnIWasCookedIn = oven;
    }
    private void RemoveOven()
    {
        if (overnIWasCookedIn == null)
            return;
        overnIWasCookedIn.ObjectLeft();
        overnIWasCookedIn = null;
    }

    private void ChangeProductName(string name)
    {
        product.name = name;
    }

    public void BurnItem()
    {
        burned = true;
    }

    public bool GetIsBurned()
    {
        return burned;
    }
}
