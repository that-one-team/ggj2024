using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public static NPCSpawner Instance { get; private set; }
    public List<NPCBehavior> NPCPool { get; private set; } = new();

    [Header("Debug")]
    [SerializeField] private int _maxNPCCount = 20;
    [SerializeField] private bool _spawnOnStart;

    void Start()
    {
        if (_spawnOnStart) SpawnNPCs();
    }


    public void SpawnNPCs()
    {
        foreach (var npc in NPCPool)
        {
            Destroy(npc.gameObject);
        }

        NPCPool.Clear();
        NPCPool = new(_maxNPCCount);

        for (int i = 0; i < _maxNPCCount; i++)
        {
            var spawned = new GameObject("Spawned NPC", typeof(NPCBehavior));
            var behavior = spawned.GetComponent<NPCBehavior>();
            behavior.Spawn(transform);
        }
    }
}
