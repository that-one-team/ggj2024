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
        var path = AssetDatabase.GUIDToAssetPath(AssetDatabase.CreateFolder("Assets/Resources/Items/Humor", ItemName));

        // GameObject go =
    }
}