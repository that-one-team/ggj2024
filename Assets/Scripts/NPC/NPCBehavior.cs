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

[RequireComponent(typeof(CapsuleCollider))]
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
        SpawnClothingItem("Species");
        SpawnClothingItem("Hats", 0.02f);
        SpawnClothingItem("Neckpieces", 0.02f);
        SpawnClothingItem("Bodies", 0.01f);

        ClothingItemData SpawnClothingItem(string clothingName, float offset = 0)
        {
            var item = Resources.LoadAll<ClothingItemData>("Items/Clothing/" + clothingName).SelectRandom();
            var spawnedItem = new GameObject(item.ItemName, typeof(ClothingItemBehaviour));
            spawnedItem.transform.position = transform.position + (Vector3.forward * -offset);
            spawnedItem.transform.parent = transform;
            spawnedItem.GetComponent<ClothingItemBehaviour>().Data = item;
            spawnedItem.transform.localScale = Vector3.one;

            Data.Clothes.Add(item);
            return item;
        }
    }
}
