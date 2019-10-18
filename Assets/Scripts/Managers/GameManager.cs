using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SnakeManager _snakeManager;
    [SerializeField] private BonusManager _bonusManager;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private UIManager _uiManager;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void LoadMainMenu()
    {
        CleanStuff();
    }

    private void CleanStuff()
    {
        _snakeManager.CleanSnake();
        _gridManager.CleanGrid();
        _bonusManager.CleanBonuses();
    }

    public void StartGame()
    {
        _uiManager.ToggleMenu(false);
        CleanStuff();
        _gridManager.InitiateLevel();
    }
    public void GameOver()
    {
        _uiManager.ToggleMenu(true);
        LoadMainMenu();
    }
}
