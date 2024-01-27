using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [field: SerializeField]
    public List<ItemData> Items { get; private set; } = new();

    [Header("Settings")]
    public int MaxItemCount;

    [Header("UI")]
    [SerializeField] private Transform _inventoryPanel;
    [SerializeField] private GameObject _inventoryItemPrefab;

    public bool AddItem(ItemData item)
    {
        if (Items.Contains(item) || Items.Count >= MaxItemCount) return false;

        // TODO add notification
        print("Added item to inventory");
        Items.Add(item);

        UpdateUI();
        return true;
    }

    public void RemoveItem(ItemData item)
    {
        if (!Items.Contains(item)) return;

        // TODO add notification
        UpdateUI();
        Items.Remove(item);
    }

    void UpdateUI()
    {
        foreach (Transform child in _inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in Items)
        {
            Instantiate(_inventoryItemPrefab, _inventoryPanel).GetComponent<UIInventoryItem>().Data = item;
        }
    }

    public void UseItem(ItemData item, NPCBehavior targetNPC = null)
    {
        if (!Items.Contains(item)) return;

        var isNegativelyAffected = HumorStats.GetMaxStat(item.AffectedStats).StatName != HumorStats.GetMaxStat(targetNPC.Data.Stats).StatName;
        var moodImpact = isNegativelyAffected ? Random.Range(-40, -10) : Random.Range(5, 20);
        targetNPC.GetComponent<NPCMood>().AddMood(moodImpact);

        var reputationImpact = isNegativelyAffected ? -Random.Range(5, 10) : Random.Range(5, 10);
        PlayerReputation.Instance.AddRep(reputationImpact);

        targetNPC.HasInteractedAlready = !isNegativelyAffected;
        GetComponent<PlayerSocialBattery>().SocialBattery += isNegativelyAffected ? -Random.Range(5, 10) : Random.Range(10, 20);

        if (item.IsConsumable) RemoveItem(item);
    }
}