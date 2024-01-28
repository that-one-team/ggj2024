// author: @cicerolb
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSocialBattery : MonoBehaviour
{
    public float SocialBattery = 100;
    [field: SerializeField] public float PassiveDrainMult { get; private set; } = 0.5f;
    [field: SerializeField] public float ActiveDrainMult { get; private set; } = 1f;

    void Update()
    {
        SocialBattery -= Time.deltaTime * PassiveDrainMult;
        if (SocialBattery <= 0) GameManager.Instance.GameOver(OverReasons.NO_BAT);
    }
}
