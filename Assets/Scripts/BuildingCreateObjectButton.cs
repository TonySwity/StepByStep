using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreateObjectButton : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _objectPrefab;
    
    public void TryBuy()
    {
        int price = _objectPrefab.GetComponent<Unit>().Price;

        Resources playerResources = FindObjectOfType<Resources>();
        
        if (playerResources.Money >= price)
        {
            playerResources.Money -= price;
            Instantiate(_objectPrefab, _spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("No money, no honey");
        }
    }
}
