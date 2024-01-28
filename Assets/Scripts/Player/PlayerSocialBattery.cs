// author: @cicerolb
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSocialBattery : MonoBehaviour
{
    public float SocialBattery = 100;
    [field: SerializeField] public float PassiveDrainMult { get; private set; } = 0.5f;
    [field: SerializeField] public float ActiveDrainMult { get; private set; } = 1f;

    float _fakeLog = 1;

    void Update()
    {
        _fakeLog = SocialBattery <= 10 ? 0.5f : 1;

        SocialBattery -= Time.deltaTime * PassiveDrainMult * _fakeLog;
        if (SocialBattery <= 0) GameManager.Instance.GameOver(OverReasons.NO_BAT);
    }
}
