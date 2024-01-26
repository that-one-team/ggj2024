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
        NPCData data = CreateInstance<NPCData>();
        data.Stats = Stats;
        data.Name = NPCName;
        data.Clothes = Clothes;

        var path = AssetDatabase.GUIDToAssetPath(AssetDatabase.CreateFolder("Assets/Resources/NPCs", NPCName));
        AssetDatabase.CreateAsset(data, path + $"/{NPCName}Data.asset");

        GameObject go = new(NPCName);
        var npcBehavior = go.AddComponent<NPCBehavior>();
        npcBehavior.Data = data;
        go.SetActive(false);

        PrefabUtility.SaveAsPrefabAsset(go, path + $"/{NPCName}.prefab");

        AssetDatabase.SaveAssets();
        DestroyImmediate(go);

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = data;
    }
}