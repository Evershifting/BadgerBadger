using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Vector2 _gridPosition;
    private GridManager _grid;

    public bool IsEmpty { get; set; } = true;
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

    public void Init(GridManager grid, Vector2 gridPosition)
    {
        GridPosition = gridPosition;
        _grid = grid;
    }
}
