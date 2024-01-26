// author: @zsfer
using UnityEngine;

[System.Serializable]
public struct NPCData
{
    public string Name;
    public HumorStats Stats;
    public ClothingItemData[] Clothes;
}

public class NPCBehavior : MonoBehaviour
{
    [field: SerializeField] public NPCData Data { get; set; }

    public void Spawn(Transform parent)
    {
        transform.parent = parent;
        GenerateOutfit();
    }

    void GenerateOutfit()
    {
        // look through assets inside resources/items/clothing
        var species = Resources.LoadAll<ClothingItemData>("Items/Clothing/Species");
        var hats = Resources.LoadAll<ClothingItemData>("Items/Clothing/Hats");
        var necks = Resources.LoadAll<ClothingItemData>("Items/Clothing/Neckpieces");
        var bodies = Resources.LoadAll<ClothingItemData>("Items/Clothing/Bodies");

        print(species.Length);
    }
}
