// author: @zsfer
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class NPCData
{
    public string Name;
    public HumorStats Stats;
    public List<ClothingItemData> Clothes;

}

public enum NPCState
{
    IDLE = 0,
    MOVING = 1,
    INTERACTING = 2
}


[RequireComponent(typeof(CapsuleCollider), typeof(NPCMovement), typeof(NPCInteraction))]
public class NPCBehavior : MonoBehaviour
{
    [field: SerializeField] public NPCData Data { get; set; }
    public bool HasInteractedAlready { get; set; }

    [Header("Debug")]
    [SerializeField] bool _showDebug;
    [SerializeField] TextMeshProUGUI _statsDebug;

    public void Spawn(Transform parent)
    {
        transform.name = Data.Name;
        GetComponent<CapsuleCollider>().radius = 1;
        GenerateOutfit();

        Data.Stats = GetHumorSum();
    }

    public HumorStats GetHumorSum()
    {
        HumorStats stats = Data.Stats;

        foreach (var clothes in Data.Clothes)
        {
            stats.Add(clothes.Stats);
        }

        return stats;
    }

    void GenerateOutfit()
    {
        Data ??= new()
        {
            Name = MiscItems.NPCNames.SelectRandom(),
            Clothes = new()
        };

        transform.name = Data.Name;

        // look through assets inside resources/items/clothing;

        if (Data.Clothes.Count == 0)
        {
            SpawnRandomClothing("Species");
            SpawnRandomClothing("Hats", 0.02f);
            SpawnRandomClothing("Neckpieces", 0.02f);
            SpawnRandomClothing("Bodies", 0.01f);
        }
        else
        {
            foreach (var clothes in Data.Clothes)
            {
                SpawnClothingItem(clothes);
            }
        }

        ClothingItemData SpawnRandomClothing(string clothingName, float offset = 0)
        {
            var item = Resources.LoadAll<ClothingItemData>("Items/Clothing/" + clothingName).SelectRandom();
            SpawnClothingItem(item, offset);

            Data.Clothes.Add(item);
            return item;
        }
    }

    void SpawnClothingItem(ClothingItemData item, float offset = 0)
    {
        var spawnedItem = new GameObject(item.ItemName, typeof(ClothingItemBehaviour));
        spawnedItem.transform.position = transform.position + (Vector3.forward * -item.Offset);
        spawnedItem.transform.parent = transform;
        spawnedItem.GetComponent<ClothingItemBehaviour>().Data = item;

        spawnedItem.GetComponent<ClothingItemBehaviour>().UpdateVisuals();
    }

    void Update()
    {
#if UNITY_EDITOR
        _statsDebug.text = _showDebug ? $"{Data.Stats} - [MOOD] {GetComponent<NPCMood>().CurrentMood}" : "";
#endif
    }


}

