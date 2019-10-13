using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int _sizeX, _sizeY;
    [SerializeField] private Cell _cellPrefab;
    private List<Cell> _cells = new List<Cell>();

    private Vector3 _cell0x0Position;
    private void Start()
    {
        GenerateField(_sizeX, _sizeY);
    }

    private void GenerateField(int SizeX, int SizeY)
    {
        Calculate0x0(SizeX, SizeY);
        for (int i = 0; i < SizeX; i++)
        {
            for (int j = 0; j < SizeY; j++)
            {
                _cells.Add(Instantiate(_cellPrefab, _cell0x0Position + Vector3.right * i + Vector3.forward * j, _cellPrefab.transform.rotation, transform));
                _cells[_cells.Count - 1].name = string.Format("Cell {0}x{1}", i, j);
            }
        }
    }

    private void Calculate0x0(int sizeX, int sizeY)
    {
        _cell0x0Position = -1 * new Vector3((float)sizeX / 2 - _cellPrefab.transform.localScale.x / 2, 0, (float)sizeY / 2 - _cellPrefab.transform.localScale.y / 2);
    }

    private void SpawnSnake()
    {

    }

}
