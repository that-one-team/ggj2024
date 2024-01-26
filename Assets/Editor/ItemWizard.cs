using UnityEngine;
using UnityEditor;

public class ItemWizard : ScriptableWizard
{
    public string ItemName;
    public HumorStats Stats;
    public Sprite InventoryIcon;

    [MenuItem("TOT/Create Item")]
    private static void MenuEntryCall()
    {
        DisplayWizard<ItemWizard>("Create Item");
    }

    private void OnWizardCreate()
    {
        var path = "Assets/Resources/Items/Humor/";
        var prefabsPath = "Assets/Resources/Prefabs/Items";

        GameObject go = new(ItemName);
        var behavior = go.AddComponent<ItemBehaviour>();

        var data = CreateInstance<ItemData>();
        data.Name = ItemName;
        data.AffectedStats = Stats;
        data.Sprite = InventoryIcon;

        AssetDatabase.CreateAsset(data, path + $"/{ItemName}.asset");
        behavior.UpdateVisuals(data);

        PrefabUtility.SaveAsPrefabAsset(go, prefabsPath + $"/{ItemName}.prefab");

        AssetDatabase.SaveAssets();
    }
}