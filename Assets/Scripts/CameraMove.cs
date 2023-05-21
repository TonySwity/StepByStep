using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Camera _rayCastCamera;

    private Vector3 _startPoint;
    private Vector3 _cameraStartPosition;
    private Plane _plane;

    private void Start()
    {
        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update()
    {
        Ray ray = _rayCastCamera.ScreenPointToRay(Input.mousePosition);

        float distance;
        _plane.Raycast(ray, out distance);
        Vector3 point = ray.GetPoint(distance);

        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Space))
        {
            _startPoint = point;
            _cameraStartPosition = transform.position;
        }

        if (Input.GetMouseButton(2) || Input.GetKey(KeyCode.Space))
        {
            Vector3 offset = point - _startPoint;
            transform.position = _cameraStartPosition - offset;
        }
        
        transform.Translate(0,0, Input.mouseScrollDelta.y);
        _rayCastCamera.transform.Translate(0f,0f, Input.mouseScrollDelta.y);
    }
}
