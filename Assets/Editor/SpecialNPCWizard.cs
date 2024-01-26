// author: @zsfer
using UnityEngine;
using UnityEditor;
using System.Linq;

public class SpecialNPCWizard : ScriptableWizard
{
    public string NPCName;
    public HumorStats Stats;
    public ClothingItemData[] Clothes;

    [MenuItem("TOT/Create Special NPC")]
    private static void MenuEntryCall()
    {
        DisplayWizard<SpecialNPCWizard>("Create Special NPC");
    }

    private void OnWizardCreate()
    {
        var path = "Assets/Resources/NPCs";

        GameObject go = new(NPCName);
        var npcBehavior = go.AddComponent<NPCBehavior>();

        var data = new NPCData
        {
            Stats = Stats,
            Name = NPCName,
            Clothes = Clothes.ToList()
        };
        npcBehavior.Data = data;
        npcBehavior.Spawn(null);

        PrefabUtility.SaveAsPrefabAsset(go, path + $"/{NPCName}.prefab");
        DestroyImmediate(go);

        AssetDatabase.SaveAssets();
    }
}