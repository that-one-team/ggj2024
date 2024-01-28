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
    [SerializeField] GameObject _pauseScreen;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        _gameOverScreen.SetActive(false);
        _pauseScreen.SetActive(false);
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            PauseGame(!_pauseScreen.activeSelf);
        }
    }

    public void PauseGame(bool state)
    {
        _pauseScreen.SetActive(state);

        Time.timeScale = state ? 0 : 1;
    }
}