using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private Vector3 spawnOffset;
    [SerializeField] private GameObject spawnObject;
    public void SpawnObjectWithOffset()
    {
        Vector3 positionToSpawn = transform.position + spawnOffset;
        Spawn(positionToSpawn, spawnObject);
    }
    private void Spawn(Vector3 positionToSpawn, GameObject objectToSpawn)
    {
        Instantiate(objectToSpawn, positionToSpawn, Quaternion.identity);
    }
}
