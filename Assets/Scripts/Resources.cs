using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Resources : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyInfo;
    [FormerlySerializedAs("Money")]
    private int _money = 15;

    private void Start()
    {
        _moneyInfo.text = _money.ToString();
    }

    public bool TryBuy(int value)
    {
        if (_money >= value)
        {
            _money -= value;

            _moneyInfo.text = _money.ToString();
            return true;
        }
        
        if (_money == 0)
        {
            _moneyInfo.text = "No money, no honey";
        }

        return false;
    }

    public void AddCoins(int value)
    {
        _money += value;
        _moneyInfo.text = _money.ToString();
    }
}
