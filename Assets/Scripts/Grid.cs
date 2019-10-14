using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int _sizeX, _sizeY;
    [SerializeField] private GameObject _cellPrefab, _snakeSegment, _snakeHead;

    public List<Cell> Cells  = new List<Cell>();

    private void Start()
    {
        GenerateField(_sizeX, _sizeY);
        SpawnSnake();
    }

    private void GenerateField(int SizeX, int SizeY)
    {
        GameObject gameObject;
        for (int i = 0; i < SizeX; i++)
        {
            for (int j = 0; j < SizeY; j++)
            {
                gameObject = Instantiate(_cellPrefab, Cell0x0() + Vector3.right * i + Vector3.forward * j, _cellPrefab.transform.rotation, transform);
                gameObject.AddComponent<Cell>().Init(this, new Vector2(i, j));
                Cells.Add(gameObject.GetComponent<Cell>());
            }
        }
    }
    private void SpawnSnake()
    {
        GameObject go1, go2;
        go1 = Instantiate(_snakeHead);
        go1.GetComponent<SnakeHead>().Grid = this;
        go1.GetComponent<SnakeHead>()._cell = GetCellByVector2(new Vector2(_sizeX / 2, _sizeY / 2));
        go1.transform.position = go1.GetComponent<SnakeHead>()._cell.Position;

        for (int i = 1 ; i <= _sizeY / 2; i++)
        {
            go2 = Instantiate(_snakeSegment);
            go2.GetComponent<SnakeSegment>()._cell = GetCellByVector2(new Vector2(_sizeX / 2, _sizeY / 2 - i));
            go2.transform.position = go2.GetComponent<SnakeSegment>()._cell.Position;
            if (i==1)
                go1.GetComponent<SnakeHead>().PreviousSegment = go2.GetComponent<SnakeSegment>();
            else
                go1.GetComponent<SnakeSegment>().PreviousSegment = go2.GetComponent<SnakeSegment>();
            go1 = go2;
        }
    }

    private Vector3 Cell0x0()
    {
        return new Vector3((float)_sizeX / 2 - _cellPrefab.transform.localScale.x / 2, 0, (float)_sizeY / 2 - _cellPrefab.transform.localScale.y / 2) * (-1);
    }

    public Cell GetNextCell(Cell currentPostion, Vector3 direction)
    {
        Vector2 nextCellPosition = currentPostion.GridPosition + new Vector2(direction.x, direction.z);

        if (nextCellPosition.x >= _sizeX)
            nextCellPosition.x = 0;
        if (nextCellPosition.y >= _sizeY)
            nextCellPosition.y = 0;

        if (nextCellPosition.x < 0)
            nextCellPosition.x = _sizeX-1;
        if (nextCellPosition.y < 0)
            nextCellPosition.y = _sizeY-1;

        return GetCellByVector2(nextCellPosition);
    }
    private Cell GetCellByVector2(Vector2 position)
    {
        return Cells[(int)position.x * _sizeY + (int)position.y];
    }
}
