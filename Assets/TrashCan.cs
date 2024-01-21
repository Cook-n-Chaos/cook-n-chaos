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
            AudioManager.Instance.Play("poof");
            Destroy(go);
            return;
        }
        if(go.layer == 6)
        {
            Instantiate(_trashItemParticles, go.transform.position, Quaternion.identity);
            if (go.transform.parent.GetComponent<SliceCounter>() != null)
            {
                if(go.transform.parent.childCount > 1)
                {
                    Destroy(go);
                }
                else
                {
                    Destroy(go.transform.parent.gameObject);
                }
            }
        }
    }
}
