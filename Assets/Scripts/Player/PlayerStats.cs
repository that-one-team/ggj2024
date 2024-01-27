using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [field: SerializeField]
    public HumorStats Data { get; private set; } = new();

    // public bool AddStats(HumorStats stats)
    // {
    //     // Data.Add(stats);
    // }
}