using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private SnakeSegment _snakeSegment;
    [SerializeField] private NewSegment _newSegment;

    [Space]
    [SerializeField] private GridManager Grid;

    [Space]
    [Range(1, int.MaxValue)]
    [SerializeField] private float _defaultSpeed = 3f, _maxSpeed = 30f, _minSpeed = 1f;
    [Space]
    [SerializeField] private Color _head, _body;

    private Vector3 _currentDirection = Vector3.forward, _defaultDirection = Vector3.forward;
    private Vector3 _nextDirection = Vector3.forward;
    private float _turnTimer;
    private float _currentSpeed = 3f;

    public List<SnakeSegment> Snake { get; private set; } = new List<SnakeSegment>();
    public Cell LastCell { get; set; }
    private float Speed
    {
        get => _currentSpeed;
        set
        {
            Mathf.Clamp(value, _minSpeed, _maxSpeed);
            _currentSpeed = value;
        }
    }

    private void Update()
    {
        GetNextDirection();

        _turnTimer += Time.deltaTime;
        if (_turnTimer >= 1 / Speed)
        {
            _turnTimer = 0;

            _currentDirection = _nextDirection;
            if (Snake.Count > 0)
                Snake[0].Move(Grid.GetNextCell(Snake[0].Cell, _nextDirection));
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

    public void CleanSnake()
    {
        for (int i = Snake.Count; i > 0; i--)
        {
            Destroy(Snake[i-1].gameObject);
            Snake.RemoveAt(Snake.Count - 1);
        }
        LastCell = null;
        _currentSpeed = _defaultSpeed;
        _currentDirection = _defaultDirection;
        _nextDirection = _defaultDirection;
    }
    public void SpawnSnake()
    {
        CleanSnake();

        SnakeSegment segment;

        for (int i = 0; i <= Grid.SizeY / 2; i++)
        {
            segment = Instantiate(_snakeSegment, transform);
            segment.Cell = Grid.GetCellByVector2(new Vector2(Grid.SizeX / 2, Grid.SizeY / 2 - i));
            segment.transform.position = segment.Cell.Position;
            Grid.GetCellByVector2(new Vector2(Grid.SizeX / 2, Grid.SizeY / 2 - i)).IsEmpty = false;
        }

        ConnectSegments();
        ColorSnake();
    }

    private void ConnectSegments()
    {
        for (int i = 0; i < Snake.Count - 1; i++)
        {
            Snake[i].PreviousSegment = Snake[i + 1];
        }
    }

    private void ColorSnake()
    {
        for (int i = 0; i < Snake.Count; i++)
        {
            Snake[i].GetComponent<Renderer>().material.color = Color.Lerp(_head, _body, i / (float)Snake.Count);
        }
    }

    #region Bonus Interaction
    public void ReverseHead()
    {
        Snake.Reverse();
        Snake[Snake.Count - 1].PreviousSegment = null;
        ConnectSegments();
        ColorSnake();

        _currentDirection *= (-1);
        _nextDirection *= (-1);
    }

    public void ChangeSpeed(float value, float duration)
    {
        if (value <= 0)
            value = 1f;
        if (duration < 0)
            duration = 0;
        StartCoroutine(ChangeSpeedCoroutine(value, duration));
    }

    private IEnumerator ChangeSpeedCoroutine(float value, float duration)
    {
        Speed *= value;
        yield return new WaitForSeconds(duration);
        Speed /= value;
    }

    public void AddSegment()
    {
        NewSegment ns = Instantiate(_newSegment);
        ns.Path = Snake.Select(seg => seg.Cell).ToList();
        ns.Speed = Speed;
        ns.GoForthToCertainDeath(OnNewSegmentReadyToSpawn, _head, _body);
    }

    private void OnNewSegmentReadyToSpawn()
    {
        SnakeSegment segment = Instantiate(_snakeSegment, LastCell.transform.position, Quaternion.identity, transform);
        segment.Cell = LastCell;
        ConnectSegments();
        ColorSnake();
    }

    public void RemoveSegment()
    {
        if (Snake.Count > 0)
        {
            Snake[Snake.Count - 1].DeathEffect(OnDeathAnimationEnd, 1 / Speed);
            LastCell = Snake[Snake.Count - 1].Cell;
        }
    }

    private void OnDeathAnimationEnd()
    {
        if (Snake.Count > 1)
        {
            Snake[Snake.Count - 2].PreviousSegment = null;
            Destroy(Snake[Snake.Count - 1].gameObject);
            Snake.RemoveAt(Snake.Count - 1);
            ColorSnake();
        }
        else
            GameManager.Instance.GameOver();
    }
    #endregion
}
