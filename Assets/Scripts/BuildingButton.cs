using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    public BuildingPlacer BuildingPlacer;
    public Building BuildingPrefab;
    [SerializeField] private TextMeshProUGUI _priceInfo;
    
    private Resources _playerResources;

    private void Start()
    {
        _priceInfo.text = BuildingPrefab.Price.ToString("00");
        _playerResources = FindAnyObjectByType<Resources>();
    }

    public void TryBuy()
    {
        int price = BuildingPrefab.Price;

        if (_playerResources.TryBuy(price))
        {
            BuildingPlacer.CreateBuilding(BuildingPrefab);
        }
    }
}
