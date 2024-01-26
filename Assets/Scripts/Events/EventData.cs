using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event Data", menuName = "Event Data", order = 0)]
public class EventData : ScriptableObject
{
    public string EventName;
    public HumorStats StatsEffect;
    public float ReputationEffect;

    public float EventChance;
}
