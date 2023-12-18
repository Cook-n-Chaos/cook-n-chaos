using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StackingItemPlace : StoredIngriedeints
{
    private string stackableTag = "Stackable";
    private float stackOffset = 0.015f;

    [SerializeField] private Transform startingPlacingPosition;
    private Vector3 currentStackPosition = Vector3.zero;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(stackableTag))
        {
            StackObject(other.gameObject);
        }
    }

    private void StackObject(GameObject obj)
    {
        // Get the collider and size of the object

        // Calculate the offset for stacking
        Vector3 stackOffsetVector = new Vector3(0, stackOffset + 0.0025f, 0);
        if (currentStackPosition == Vector3.zero)
            stackOffsetVector -= new Vector3(0, stackOffset, 0);
        // Calculate the new position for stacking
        currentStackPosition += stackOffsetVector;

        

        

        // Disable the XRGrabInteractable component (if it exists)
        XRGrabInteractable grab = obj.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            Destroy(grab);
        }

        // Disable the Rigidbody component (if it exists)
        Rigidbody objRigidbody = obj.GetComponent<Rigidbody>();
        if (objRigidbody != null)
        {
            Destroy(objRigidbody);
        }

        // Disable the Collider component (if it exists)
        Collider objCollider = obj.GetComponent<Collider>();
        if (objCollider != null)
        {
            Destroy(objCollider);
        }

        // Set the object's rotation to the stacking position
        obj.transform.rotation = Quaternion.identity;

        // Set the new position for the object
        obj.transform.position = startingPlacingPosition.position + currentStackPosition;

        
        // Parent the object to this GameObject
        obj.transform.parent = transform;
        AddIngredient(obj.GetComponent<Product>().name);
        
    }
}
