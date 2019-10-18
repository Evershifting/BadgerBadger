using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private List<Bonus> _bonuses;
    [SerializeField] private GridManager grid;
    [SerializeField] private float _bonusLifetime = 20f, _spawnInterval = 5f;

    private float _currentSpawnTimer = 0f;

    private void Update()
    {
        _currentSpawnTimer += Time.deltaTime;
        if (_currentSpawnTimer > _spawnInterval)
        {
            _currentSpawnTimer = 0;
            SpawnBonus();
        }
    }

    private void SpawnBonus()
    {
        if (!grid.GetEmptyCell())
            return;
        Debug.Log("SpawnBonus after gridcheck");
        float totalWeight = 0f;
        float usefulWeight = 0f;

        foreach (Bonus bonus in _bonuses)
        {
            totalWeight += bonus.GenerationWeight;
        }
        float randomResult = Random.Range(0, totalWeight);

        foreach (Bonus bonus in _bonuses)
        {
            if (randomResult <= bonus.GenerationWeight + usefulWeight)
            {
                Debug.Log("In if");
                Bonus spawnedBonus = Instantiate(bonus, transform);
                spawnedBonus.BonusManager = this;
                Cell cell = grid.GetEmptyCell();
                cell.IsEmpty = false;
                spawnedBonus.Cell = cell;
                spawnedBonus.transform.position = cell.transform.position;
                spawnedBonus.SetLifeCycleDuration(_bonusLifetime);
                break;
            }
            usefulWeight += bonus.GenerationWeight;
        }
    }
    public void ExecuteBonusEffect(Bonus bonus)
    {
        bonus.ExecuteBonus();
    }

}
