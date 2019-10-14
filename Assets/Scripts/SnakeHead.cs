using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public Vector3 _currentDirection = Vector3.forward;
    public Vector3 _nextDirection = Vector3.forward;
    private float _turnTimer;
    [SerializeField] private float _speed = 1f;

    public Cell _cell;
    public Grid Grid;
    public SnakeSegment PreviousSegment;


    private void Update()
    {
        GetNextDirection();

        _turnTimer += Time.deltaTime;
        if (_turnTimer >= 1 / _speed)
        {
            _turnTimer = 0;
            Debug.Log(Grid.GetNextCell(_cell, _nextDirection));
            Move(Grid.GetNextCell(_cell, _nextDirection));
        }            
    }

    private void GetNextDirection()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && _currentDirection!= Vector3.left)
            _nextDirection = Vector3.right;
        else if (Input.GetAxisRaw("Horizontal") < 0 && _currentDirection != Vector3.right)
            _nextDirection = Vector3.left;
        else if (Input.GetAxisRaw("Vertical") > 0 && _currentDirection != Vector3.back)
            _nextDirection = Vector3.forward;
        else if (Input.GetAxisRaw("Vertical") < 0 && _currentDirection != Vector3.forward)
            _nextDirection = Vector3.back;
    }

    private void Move(Cell destination)
    {
        _currentDirection = _nextDirection;
        transform.position = destination.Position;
        PreviousSegment.Move(_cell);
        _cell = destination;
    }
}
