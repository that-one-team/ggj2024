using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum OverReasons
{
    NO_REP,
    MAX_REP,
    NO_BAT,
    MAX_STAT,
}

public class GameManager : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] GameObject _gameOverScreen;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        _gameOverScreen.SetActive(false);
    }
    public static event Action<OverReasons> OnGameOver;

    public void GameOver(OverReasons reason)
    {
        OnGameOver?.Invoke(reason);
        _gameOverScreen.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("SCN_TitleScreen");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}