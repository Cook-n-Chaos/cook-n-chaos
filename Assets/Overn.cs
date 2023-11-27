using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.layer == 8)
        {
            ChangeToCooked toCook = go.GetComponent<ChangeToCooked>();
            toCook.ChangeToCookedModel();
        }
    }
}
