using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    [SerializeField] private float _generationWeight;

    public float GenerationWeight { get => _generationWeight; }
    public BonusManager BonusManager { get; set; }
    public Cell Cell { get; set; }
    
    internal virtual void ExecuteBonus()
    {
        StopAllCoroutines();
        DestroyBonus();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPickup();
    }

    internal virtual void OnPickup()
    {
        AudioManager.Instance.Pop();
        BonusManager.ExecuteBonusEffect(this);
    }

    public void SetLifeCycleDuration(float duration)
    {
        StartCoroutine(LifeCycle(duration));
    }

    private float durationLeft;
    private IEnumerator LifeCycle(float duration)
    {
        durationLeft = duration;
        while (durationLeft > 0)
        {
            durationLeft-= Time.deltaTime;
            yield return null;
        }
        DestroyBonus();
    }

    internal void DestroyBonus()
    {
        Cell.IsEmpty = true;
        Destroy(gameObject);
    }
}