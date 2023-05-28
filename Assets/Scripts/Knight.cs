using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public enum UnitState
{
    Idle,
    WalkToPoint,
    WalkToEnemy,
    Attack
}

public class Knight : Unit
{
    public UnitState CurrentUnitState;
    public Vector3 TargetPoint;
    public Enemy TargetEnemy;
    public float DistanceToFollow = 7f;
    public float DistanceToAttack = 1f;

    //public NavMeshAgent NavMeshAgent;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackPeriod =1f;
    private float _timer;

    public override void Start()
    {
        base.Start();
        SetState(UnitState.WalkToPoint);
    }

    private void Update()
    {
        if (CurrentUnitState == UnitState.Idle)
        {
            FindClosestEnemy();
           // FindClosestBuilding();
           
        }
        else if (CurrentUnitState == UnitState.WalkToPoint)
        {
            FindClosestEnemy();
            
        }
        else if (CurrentUnitState == UnitState.WalkToEnemy)
        {
            if (TargetEnemy)
            {
                this.NavMeshAgent.SetDestination(TargetEnemy.transform.position);
                float distance = Vector3.Distance(transform.position, TargetEnemy.transform.position);

                if (distance > DistanceToFollow)
                {
                    SetState(UnitState.WalkToPoint);
                }
                if (distance <= DistanceToAttack)
                {
                    SetState(UnitState.Attack);
                }
            }
        }
        else if (CurrentUnitState == UnitState.Attack)
        {
            if (TargetEnemy)
            {
                this.NavMeshAgent.SetDestination(TargetEnemy.transform.position);
                float distance = Vector3.Distance(transform.position, TargetEnemy.transform.position);

                if (distance >= DistanceToAttack)
                {
                    SetState(UnitState.WalkToEnemy);
                }

                _timer += Time.deltaTime;

                if (_timer > _attackPeriod)
                {
                    _timer = 0;
                    TargetEnemy.TakeDamage(_damage);
                }
            }
            else
            {
                SetState(UnitState.WalkToPoint);
            }
        }
    }

    public void SetState(UnitState unitState)
    {
        CurrentUnitState = unitState;

        switch (CurrentUnitState)
        {
            case UnitState.Idle:
                break;
            case UnitState.WalkToPoint:

                break;
            case UnitState.WalkToEnemy:
                break;
            case UnitState.Attack:
                _timer = 0;
                break;
        }
    }

    public void FindClosestEnemy()
    {
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        float minDistance = Mathf.Infinity;
        Enemy closestEnemy = null;

        foreach (var enemy in allEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (minDistance < DistanceToFollow)
        {
            this.TargetEnemy = closestEnemy;
            SetState(UnitState.WalkToEnemy);
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