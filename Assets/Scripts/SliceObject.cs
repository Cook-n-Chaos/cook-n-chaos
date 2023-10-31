using EzySlice;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask slicableLayer;

    public float cutForce = 2000f;
    private void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, slicableLayer);
        if(hasHit)
        {
            Debug.Log("Object Found");
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        Material crossSectionMaterial = target.GetComponent<SlicableObject>().slicedMaterial;
        float mass = target.GetComponent<Rigidbody>().mass;

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        SlicableObject targetSlicableObject = target.GetComponent<SlicableObject>();
        SliceCounter counter = targetSlicableObject.parentHolder;
        if (counter.IncreaseSliceAmount())
        {
            counter.ChangeObject(target.transform.position);
            return;
        }
          
      

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            upperHull.transform.parent = targetSlicableObject.parentHolder.transform;
            SetupSlicedComponent(upperHull, crossSectionMaterial, mass);

            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            lowerHull.transform.parent = targetSlicableObject.parentHolder.transform;
            SetupSlicedComponent(lowerHull, crossSectionMaterial, mass);

            StartCoroutine(SetSlicable(upperHull, lowerHull, 0.35f));

            
           

            Destroy(target);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject, Material parentSlicedMaterial, float parentMass)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        rb.mass = parentMass / 2 < 1 ? 1 : parentMass / 2;

        SlicableObject slicableObject = slicedObject.AddComponent<SlicableObject>();
        slicableObject.parentHolder = slicableObject.transform.parent.GetComponent<SliceCounter>(); 
        slicableObject.slicedMaterial = parentSlicedMaterial;

        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        slicableObject.AddComponent<XRGrabInteractable>();
        if(collider.sharedMesh.vertexCount >=4)
            collider.convex = true;

        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
    IEnumerator SetSlicable(GameObject upperHull, GameObject lowerHull, float time)
    {
        yield return new WaitForSeconds(time);
        upperHull.layer = 6;
        lowerHull.layer = 6;
    }
}
