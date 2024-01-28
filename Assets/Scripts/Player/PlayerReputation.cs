// author: @cicerolb
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReputation : MonoBehaviour
{
    public static PlayerReputation Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public float Reputation { get; private set; } = 10;

    public void AddRep(float value)
    {
        print("[REP]: added rep " + value);
        Reputation += value;

        if (Reputation <= 0)
        {
            GameManager.Instance.GameOver(OverReasons.NO_REP);
        }
    }

}
