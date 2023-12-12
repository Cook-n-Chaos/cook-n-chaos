using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] ParticleSystem _trashItemParticles;
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.layer == 7 || go.tag == "Stackable")
        {
            Instantiate(_trashItemParticles, go.transform.position, Quaternion.identity);
            Destroy(go);          
        }
    }
}
