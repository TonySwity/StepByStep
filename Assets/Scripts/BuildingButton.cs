using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    public BuildingPlacer BuildingPlacer;
    public GameObject BuildingPrefab;

    public void TryBuy()
    {
        int price = BuildingPrefab.GetComponent<Building>().Price;

        Resources playerResources = FindObjectOfType<Resources>();
        
        if (playerResources.Money >= price)
        {
            playerResources.Money -= price;
            BuildingPlacer.CreateBuilding(BuildingPrefab);
        }
        else
        {
            Debug.Log("No money, no honey");
        }
    }
}
