using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform _scaleTransform;
    [SerializeField] private Transform _target;

    private Transform _cameraTransform;


    private void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (_target)
        {
            transform.position = _target.position + Vector3.up * 2f;
            // Vector3 toCamera = transform.position - _cameraTransform.position;
            // transform.rotation = Quaternion.LookRotation(toCamera);
            transform.rotation = _cameraTransform.rotation;
        }
    }

    public void Setup(Transform target)
    {
        _target = target;
    }

    public void SetHealth(int health, int maxHealth)
    {
        float xScale = health / (float)maxHealth;
        xScale = Mathf.Clamp01(xScale); 
        _scaleTransform.localScale = new Vector3(xScale, 1f, 1f);
    }

}
