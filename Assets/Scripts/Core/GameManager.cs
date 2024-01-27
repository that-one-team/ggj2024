using System;
using UnityEngine;

public enum OverReasons
{
    NO_REP,
    MAX_REP,
    NO_BAT,
    MAX_STAT,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public static event Action<OverReasons> OnGameOver;

    public void GameOver(OverReasons reason)
    {
        OnGameOver?.Invoke(reason);
    }
}