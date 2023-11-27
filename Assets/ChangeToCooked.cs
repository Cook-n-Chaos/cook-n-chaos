using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToCooked : MonoBehaviour
{
    [SerializeField] private GameObject modelBefore;
    [SerializeField] private GameObject modelAfter;
    private Product product;
    [SerializeField] private string toChangeProductName = "empty";

    private void Awake()
    {
        product = gameObject.GetComponent<Product>();
    }
    public void ChangeToCookedModel()
    {
        Vector3 position = modelBefore.transform.position;
        Quaternion rotation = modelBefore.transform.rotation;

        Instantiate(modelAfter, position, rotation, transform);
        gameObject.layer = 0;
        ChangeProductName();

        Destroy(modelBefore);
    }

    private void ChangeProductName()
    {
        product.name = toChangeProductName;
    }
}
