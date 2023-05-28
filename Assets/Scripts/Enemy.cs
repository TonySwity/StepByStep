using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    WalkToBuilding,
    WalkToUnit,
    Attack
}

public class Enemy : MonoBehaviour
{
    
    
    public EnemyState CurrentEnemyState;
    public int Health;
    public Building TargetBuilding;
    public Unit TargetUnit;
    public float DistanceToFollow = 7f;
    public float DistanceToAttack = 1f;
    public GameObject HealthBarPrefab;
    public NavMeshAgent NavMeshAgent;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackPeriod =1f;
    private int _maxHealth;
    private float _timer;
    private HealthBar _healthBar;

    private void Start()
    {
        SetState(EnemyState.WalkToBuilding);
        _maxHealth = Health;
        GameObject healthBar = Instantiate(HealthBarPrefab);
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.Setup(transform);
    }

    private void Update()
    {
        if (CurrentEnemyState == EnemyState.Idle)
        {
            FindClosestBuilding();
            
            if (TargetBuilding)
            {
                SetState(EnemyState.WalkToBuilding);
            }
            
            FindClosestUnit();
        }
        else if (CurrentEnemyState == EnemyState.WalkToBuilding)
        {
            FindClosestUnit();

            if (TargetBuilding == null)
            {
                SetState(EnemyState.Idle);
            }
        }
        else if (CurrentEnemyState == EnemyState.WalkToUnit)
        {
            if (TargetUnit)
            {
                this.NavMeshAgent.SetDestination(TargetUnit.transform.position);
                float distance = Vector3.Distance(transform.position, TargetUnit.transform.position);

                if (distance > DistanceToFollow)
                {
                    SetState(EnemyState.WalkToBuilding);
                }
                if (distance <= DistanceToAttack)
                {
                    SetState(EnemyState.Attack);
                }
            }
        }
        else if (CurrentEnemyState == EnemyState.Attack)
        {
            if (TargetUnit)
            {
                this.NavMeshAgent.SetDestination(TargetUnit.transform.position);
                float distance = Vector3.Distance(transform.position, TargetUnit.transform.position);

                if (distance >= DistanceToAttack)
                {
                    SetState(EnemyState.WalkToUnit);
                }

                _timer += Time.deltaTime;

                if (_timer > _attackPeriod)
                {
                    _timer = 0;
                    TargetUnit.TakeDamage(_damage);
                }
            }
            else
            {
                SetState(EnemyState.WalkToBuilding);
            }
        }
    }

    public void SetState(EnemyState enemyState)
    {
        CurrentEnemyState = enemyState;

        switch (CurrentEnemyState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.WalkToBuilding:
                FindClosestBuilding();
                
                if (TargetBuilding)
                {
                    this.NavMeshAgent.SetDestination(this.TargetBuilding.transform.position);
                }
                else
                {
                    SetState(EnemyState.Idle);
                }
               
                break;
            case EnemyState.WalkToUnit:
                break;
            case EnemyState.Attack:
                _timer = 0;
                break;
        }
    }

    public void FindClosestBuilding()
    {
        Building[] allBuilding = FindObjectsOfType<Building>();

        float minDistance = Mathf.Infinity;
        Building closestBuilding = null;

        for (int i = 0; i < allBuilding.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, allBuilding[i].transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestBuilding = allBuilding[i];
            }
        }

        this.TargetBuilding = closestBuilding;
    }

    public void FindClosestUnit()
    {
        Unit[] allUnits = FindObjectsOfType<Unit>();

        float minDistance = Mathf.Infinity;
        Unit closestUnit = null;

        foreach (var unit in allUnits)
        {
            float distance = Vector3.Distance(transform.position, unit.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestUnit = unit;
            }
        }

        if (minDistance < DistanceToFollow)
        {
            this.TargetUnit = closestUnit;
            SetState(EnemyState.WalkToUnit);
        }
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
        if (_healthBar)
        {
            Destroy(_healthBar.gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToAttack);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToFollow);
        
    }
#endif
}
