using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPCSpawner))]
public class NPCSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        NPCSpawner targ = target as NPCSpawner;
        if (GUILayout.Button("Create Zone"))
        {
            GameObject spawnZone = new("Spawn Zone", typeof(SpawnZone));
            targ.SpawnZones.Add(spawnZone);
        }
    }
}