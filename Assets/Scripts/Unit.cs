using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Unit : SelectableObject
{
    public NavMeshAgent NavMeshAgent;
    public int Price;
    public int Health = 10;
    [SerializeField] private int _maxHealth = 10; 
    public GameObject HealthBarPrefab;
    
    private HealthBar _healthBar;
    private Management _management;

    public override void Start()
    {
        base.Start();
        _maxHealth = Health;
        GameObject healthBar = Instantiate(HealthBarPrefab);
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.Setup(transform);
        _management = FindObjectOfType<Management>();
    }

    public override void WhenClickOnGround(Vector3 point)
    {
        base.WhenClickOnGround(point);
        NavMeshAgent.SetDestination(point);
    }

    public void TakeDamage(int damageValue)
    {
        this.Health -= damageValue;
        _healthBar.SetHealth(Health, _maxHealth);
        if (this.Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _management.Unselect(this);
        if (_healthBar)
        {
            Destroy(_healthBar.gameObject);
        }
        
    }
}