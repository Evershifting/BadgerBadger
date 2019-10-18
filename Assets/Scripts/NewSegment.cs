using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSegment : MonoBehaviour
{
    [SerializeField] private GameObject _body;
    private Color _startColor, _endColor;
    public List<Cell> Path { get; set; }
    public float TimeForPath { get; set; }
    public float Speed { get; internal set; }

    private IEnumerator GoThroughPath(Action Callback)
    {
        WaitForSeconds delay = new WaitForSeconds(1 / (Speed * (Path.Count + 1)));
        Renderer renderer = _body.GetComponent<Renderer>();
        for (int i = 0; i < Path.Count; i++)
        {
            transform.position = Path[i].Position + Vector3.up;
            renderer.material.color = Color.Lerp(_startColor, _endColor, i / (float)Path.Count);
            yield return delay;
        }
        transform.position -= Vector3.up;
        Callback?.Invoke();
        Destroy(gameObject);
    }

    internal void GoForthToCertainDeath(Action addSegment, Color start, Color end)
    {
        //this choice of colors is NOT an error =)
        _startColor = end;
        _endColor = start;

        StartCoroutine(GoThroughPath(addSegment));
    }
}