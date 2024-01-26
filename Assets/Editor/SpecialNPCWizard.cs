// author: @zsfer
using UnityEngine;
using UnityEditor;

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
        var path = AssetDatabase.GUIDToAssetPath(AssetDatabase.CreateFolder("Assets/Resources/NPCs", NPCName));

        GameObject go = new(NPCName);
        var npcBehavior = go.AddComponent<NPCBehavior>();

        var data = new NPCData
        {
            Stats = Stats,
            Name = NPCName,
            Clothes = Clothes
        };
        npcBehavior.Data = data;

        go.SetActive(false);

        PrefabUtility.SaveAsPrefabAsset(go, path + $"/{NPCName}.prefab");

        AssetDatabase.SaveAssets();
        DestroyImmediate(go);

        EditorUtility.FocusProjectWindow();
    }
}