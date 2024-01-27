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

    public float Reputation = 5;

    public void AddRep(float value)
    {
        Reputation += value;
    }

}
