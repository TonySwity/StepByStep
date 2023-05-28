using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
    [SerializeField] private float _timeBetweenCreateCoin = 5f;
    [SerializeField] private int _coin = 1;
    private Resources _resources;
    private float _timer;

    public override void Start()
    {
        base.Start();
        _resources = FindObjectOfType<Resources>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeBetweenCreateCoin)
        {
            _timer = 0;
            _resources.AddCoins(_coin);   
        }
    }
}
