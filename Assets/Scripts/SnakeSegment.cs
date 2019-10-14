using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    public SnakeSegment PreviousSegment;
    public Cell _cell;

    public void Move(Vector3 destination)
    {
        if (PreviousSegment)
            PreviousSegment.Move(transform.position);
        else
        {
            if (_cell)
                _cell.IsEmpty = true;
        }
        transform.position = destination;
    }
    public void Move(Cell destination)
    {
        if (PreviousSegment)
            PreviousSegment.Move(_cell);
        else
        {
            if (_cell)
                _cell.IsEmpty = true;
        }
        _cell = destination;
        transform.position = destination.Position;
    }
}
