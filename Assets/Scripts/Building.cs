using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : SelectableObject
{
    public int Price;
    public int XSize = 3;
    public int ZSize = 3;

    private Color _startColor;
    public Renderer Renderer;

    private void Awake()
    {
        _startColor = Renderer.material.color;
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
}
