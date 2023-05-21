using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barack : Building
{
    [SerializeField] private GameObject _knightPanel;
    
    public override void Select()
    {
        base.Select();
        _knightPanel.SetActive(true);
    }

    public override void Unselect()
    {
        base.Unselect();
        _knightPanel.SetActive(false);
    }
}
