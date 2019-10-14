using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Vector2 _gridPosition;
    private bool _isEmpty = true;
    private Grid _grid;

    public Vector3 Position => transform.position;
    public Vector2 GridPosition
    {
        get => _gridPosition;
        private set
        {
            name = string.Format("Cell {0}x{1}", value.x, value.y);
            _gridPosition = value;
        }
    }
    public bool IsEmpty
    {
        get => _isEmpty;
        set
        {
            //if (value)
            //    _grid.Cells.Add(this);
            //else
            //    _grid.Cells.Remove(this);
            _isEmpty = value;
        }
    }

    public void Init(Grid grid, Vector2 gridPosition)
    {
        GridPosition = gridPosition;
        _grid = grid;
    }
}
