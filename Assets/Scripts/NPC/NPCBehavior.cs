// author: @zsfer
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    public string Name;
    public HumorStats Stats;
    public List<ClothingItemData> Clothes;

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
        Data = new()
        {
            Name = MiscItems.NPCNames.SelectRandom(),
            Clothes = new()
        };

        transform.name = Data.Name;

        // look through assets inside resources/items/clothing;
        SpawnClothingItem("Species", 0);
        SpawnClothingItem("Hats", 2);
        SpawnClothingItem("Neckpieces", 2);
        SpawnClothingItem("Bodies", 1);

        ClothingItemData SpawnClothingItem(string clothingName, int sortingOrder)
        {
            var item = Resources.LoadAll<ClothingItemData>("Items/Clothing/" + clothingName).SelectRandom();
            var spawnedItem = new GameObject(item.ItemName, typeof(ClothingItem));
            spawnedItem.transform.parent = transform;
            spawnedItem.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            spawnedItem.GetComponent<ClothingItem>().Data = item;

            Data.Clothes.Add(item);
            return item;
        }
    }
}
