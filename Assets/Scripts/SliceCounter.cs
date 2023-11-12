using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SliceCounter : MonoBehaviour
{
    public int neededSliceAmount = 3;
    [SerializeField] private int currentSliceAmount = 0;
    [SerializeField] GameObject slicedObjectPrefab;

    public bool IncreaseSliceAmount()
    {
        currentSliceAmount++;

        if (currentSliceAmount == neededSliceAmount)
        {           
            return true;
        }
        return false;
    }

    public void ChangeObject(Vector3 position)
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        GameObject slicedObject = Instantiate(slicedObjectPrefab, new Vector3(position.x, position.y + 0.2f, position.z),
            Quaternion.identity, gameObject.transform);

        slicedObject.AddComponent<XRGrabInteractable>();
        slicedObject.tag = "Stackable";
    }
}
