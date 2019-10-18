using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    public GameObject PA;
    [SerializeField] private ParticleSystem _particleSystem;
    public SnakeSegment PreviousSegment;
    public Cell Cell;
    private SnakeManager _snakeManager;

    private void OnEnable()
    {
        if (!_snakeManager)
            if (FindObjectOfType<SnakeManager>())
            {
                _snakeManager = FindObjectOfType<SnakeManager>();
                _snakeManager.Snake.Add(this);
            }
            else
                Debug.Log("No SnakeManager for lil badger!");
    }

    public virtual void Move(Cell destination)
    {
        transform.position = destination.Position;

        if (PreviousSegment)
            PreviousSegment.Move(Cell);
        else
        {
            Cell.IsEmpty = true;
            _snakeManager.LastCell = Cell;
        }

        Cell = destination;
        Cell.IsEmpty = false;
    }

    public void DeathEffect(Action callback, float duration)
    {
        StartCoroutine(DeathEffectCoroutine(callback, duration));
    }

    private IEnumerator DeathEffectCoroutine(Action callback, float duration)
    {
        _particleSystem.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        callback?.Invoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Snake")
        {
            GameManager.Instance.GameOver();
        }
    }
}
