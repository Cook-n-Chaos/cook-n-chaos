using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject stackablePlate;
    [SerializeField] private Transform placeToSpawnPlates;

    private void Start()
    {
        Instance = this;
    }

    public void SpawnStackablePlate()
    {
        Instantiate(stackablePlate, placeToSpawnPlates.position, Quaternion.identity);
    }

}
