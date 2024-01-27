// author: @cicerolb
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSocialBattery : MonoBehaviour
{
    public float SocialBattery = 100;

    void Update()
    {
        SocialBattery -= Time.deltaTime * 0.5f;
        if (SocialBattery <= 0) GameManager.Instance.GameOver(OverReasons.NO_BAT);
    }
}
