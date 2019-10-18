using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    public SnakeSegment PreviousSegment;
    public Cell _cell;

    private void OnEnable()
    {
        if (FindObjectOfType<SnakeManager>())
            FindObjectOfType<SnakeManager>()._snake.Add(this);
        else
            Debug.Log("No SnakeManager for lil badger!");
    }

    public virtual void Move(Cell destination)
    {
        transform.position = destination.Position;

        if (PreviousSegment)
            PreviousSegment.Move(_cell);
        else
        {
            _cell.IsEmpty = true;
        }

        _cell = destination;
        _cell.IsEmpty = false;
    }
}
