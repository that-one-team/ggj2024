using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPCData))]
public class NPCDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        NPCData targ = target as NPCData;

        if (GUILayout.Button("Generate Prefab"))
        {
            GameObject go = new(targ.Name);
            go.AddComponent(typeof(NPCBehavior));
        }
    }
}