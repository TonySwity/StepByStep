using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum SelectionState
{
    UnitsSelected,
    Frame,
    Other
}

public class Management : MonoBehaviour
{
    public SelectableObject Hovered;
    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();
    public Image FrameImage;
    public SelectionState CurrentState;
    private Vector2 _frameStart;
    private Vector2 _frameEnd;

    [SerializeField] private Camera _camera;

    private void Start()
    {
        FrameImage.enabled = false;
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 20f, Color.cyan);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out SelectableCollider hitSelectableCollider))
            {

                if (Hovered)
                {
                    if (Hovered != hitSelectableCollider.SelectableObject)
                    {
                        Hovered.OnUnhover();
                        Hovered = hitSelectableCollider.SelectableObject;
                        Hovered.OnHover();
                    }
                }
                else
                {
                    Hovered = hitSelectableCollider.SelectableObject;
                    Hovered.OnHover();
                }
            }
            else
            {
                UnhoveredCurrent();
            }
            
            if (CurrentState == SelectionState.UnitsSelected)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (hit.collider.tag == "Ground")
                    {
                        int rowNumber = Mathf.CeilToInt( Mathf.Sqrt(ListOfSelected.Count));
                        
                        for (int i = 0; i < ListOfSelected.Count; i++)
                        {
                            int row = i / rowNumber;
                            int column = i % rowNumber;
                            
                            Vector3 point = hit.point + new Vector3(row, 0f, column);
                            
                            ListOfSelected[i].WhenClickOnGround(point);
                            
                        }
                    }
                }
            }
        }
        else
        {
            UnhoveredCurrent();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Hovered)
            {
                if (Input.GetKey(KeyCode.LeftControl) == false)
                {
                    UnselectAll();
                }
                CurrentState = SelectionState.UnitsSelected;
                Select(Hovered);
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            UnselectAll();
        }

        //выделение рамкой
        if (Input.GetMouseButtonDown(0))
        {
            _frameStart = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            _frameEnd = Input.mousePosition;

            Vector2 min = Vector2.Min(_frameStart, _frameEnd);
            Vector2 max = Vector2.Max(_frameStart, _frameEnd);
            Vector2 size = max - min;

            if (size.magnitude > 10f)
            {
                FrameImage.enabled = true;
                FrameImage.rectTransform.anchoredPosition = min;
                FrameImage.rectTransform.sizeDelta = size;

                Rect rect = new Rect(min, size);

                UnselectAll();
                Unit[] allUnits = FindObjectsOfType<Unit>();

                for (int i = 0; i < allUnits.Length; i++)
                {
                    Vector2 screenPosition = _camera.WorldToScreenPoint(allUnits[i].transform.position);

                    if (rect.Contains(screenPosition))
                    {
                        Select(allUnits[i]);
                    }
                }
                
                CurrentState = SelectionState.Frame;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            FrameImage.enabled = false;

            if (ListOfSelected.Count > 0)
            {
                CurrentState = SelectionState.UnitsSelected;
            }
            else
            {
                CurrentState = SelectionState.Other;
            }
        }
    }

    private void Select(SelectableObject selectableObject)
    {
        if (ListOfSelected.Contains(selectableObject) == false)
        {
            ListOfSelected.Add(selectableObject);
            selectableObject.Select();
        }
    }

    public void Unselect(SelectableObject selectableObject)
    {
        if (ListOfSelected.Contains(selectableObject))
        {
            ListOfSelected.Remove(selectableObject);
        }
    }

    private void UnselectAll()
    {
        for (int i = 0; i < ListOfSelected.Count; i++)
        {
            ListOfSelected[i].Unselect();
        }

        ListOfSelected.Clear();
        CurrentState = SelectionState.Other;
    }

    private void UnhoveredCurrent()
    {
        if (Hovered)
        {
            Hovered.OnUnhover();
            Hovered = null;
        }
    }
}
