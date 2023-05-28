using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Building : SelectableObject
{
    public int Price;
    public int XSize = 3;
    public int ZSize = 3;
    
    [SerializeField] private GameObject _menuObject;
    
    private Color _startColor;
    public Renderer Renderer;

    private void Awake()
    {
        _startColor = Renderer.material.color;
    }

    public override void Start()
    {
        base.Start();
        _menuObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        float cellSize = FindObjectOfType<BuildingPlacer>().CellSize;
        
        for (int x = 0; x < XSize; x++)
        {
            for (int z = 0; z < ZSize; z++)
            {
                Gizmos.DrawWireCube(transform.position + new Vector3(x, 0, z) * cellSize, new Vector3(1, 0, 1) * cellSize);
            }
        }
    }

    public void DisplayUnacceptablePosition()
    {
        Renderer.material.color = Color.magenta;
    }
    
    public void DisplayAcceptablePosition()
    {
        Renderer.material.color = _startColor;
    }
    
    public override void Select()
    {
        base.Select();
        _menuObject.SetActive(true);
    }

    public override void Unselect()
    {
        base.Unselect();
        _menuObject.SetActive(false);
    }

}
