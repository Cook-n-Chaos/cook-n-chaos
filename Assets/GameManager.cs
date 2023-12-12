using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject stackablePlate;
    [SerializeField] private Transform placeToSpawnPlates;
    [SerializeField] private GameObject startLevelMenu;

    private void Start()
    {
        Instance = this;
    }

    public void SpawnStackablePlate()
    {
        Instantiate(stackablePlate, placeToSpawnPlates.position, Quaternion.identity);
    }

    public void ShowStartLevelMenu()
    {
        startLevelMenu.SetActive(true);
    }
    public void HideStartLevelMenu()
    {
        RecipieManager.instance.StartNewTimer();
        startLevelMenu.SetActive(false);
    }
}
