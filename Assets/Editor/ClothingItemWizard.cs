using UnityEngine;
using UnityEditor;

public class ClothingItemWizard : ScriptableWizard
{
    public string SetName;
    public Sprite Hat;
    public Sprite Neckwear;
    public Sprite Body;

    [MenuItem("TOT/Create Clothing Set")]
    private static void MenuEntryCall()
    {
        DisplayWizard<ClothingItemWizard>("Create clothing set");
    }

    private void OnWizardCreate()
    {
        CreateClothingItem("Bodies", Body);
        CreateClothingItem("Hats", Hat);
        CreateClothingItem("Neckpieces", Neckwear);

        void CreateClothingItem(string path, Sprite itemSprite)
        {
            var assetPath = $"Assets/Resources/Items/Clothing/{path}";

            var item = CreateInstance<ClothingItemData>();
            item.ItemName = $"{SetName}_{path}";
            item.Sprite = itemSprite;

            AssetDatabase.CreateAsset(item, assetPath + $"/{item.ItemName}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}