using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitButton : MonoBehaviour
{
    // [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Barack _barack;
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private TextMeshProUGUI _priceText;
    private int _price;
    private Resources _resources;
    private void Start()
    {
        _price  = _unitPrefab.GetComponent<Unit>().Price;
        _priceText.text = _price.ToString();
        _resources = FindObjectOfType<Resources>();
    }

    public void TryBuy()
    {
        if (_resources.TryBuy(_price))
        {
            _barack.CreateUnit(_unitPrefab);
        }
    }
}
