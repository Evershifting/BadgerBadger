using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public Grid Grid;
    public Vector3 _currentDirection = Vector3.forward;
    public Vector3 _nextDirection = Vector3.forward;

    [Range(1, int.MaxValue)]
    [SerializeField] private float _speed = 1f;
    private float _turnTimer;

    public List<SnakeSegment> _snake;

    private void Start()
    {
        
    }

    private void Update()
    {
        _snake[0].GetComponent<Renderer>().material.color = Color.cyan;

        GetNextDirection();

        _turnTimer += Time.deltaTime;
        if (_speed <= 0)
            _speed = 1f;
        if (_turnTimer >= 1 / _speed)
        {
            _turnTimer = 0;

            _currentDirection = _nextDirection;
            _snake[0].Move(Grid.GetNextCell(_snake[0]._cell, _nextDirection));
        }
    }

    private void GetNextDirection()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && _currentDirection != Vector3.left)
            _nextDirection = Vector3.right;
        else if (Input.GetAxisRaw("Horizontal") < 0 && _currentDirection != Vector3.right)
            _nextDirection = Vector3.left;
        else if (Input.GetAxisRaw("Vertical") > 0 && _currentDirection != Vector3.back)
            _nextDirection = Vector3.forward;
        else if (Input.GetAxisRaw("Vertical") < 0 && _currentDirection != Vector3.forward)
            _nextDirection = Vector3.back;
    }
}
