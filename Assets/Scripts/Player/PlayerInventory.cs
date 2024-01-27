using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public List<ItemData> Items { get; private set; } = new();

    public int MaxItemCount;

    public bool AddItem(ItemData item)
    {
        if (Items.Contains(item) || Items.Count >= MaxItemCount) return false;

        // TODO add notification
        print("Added item to inventory");
        Items.Add(item);
        return true;
    }

    public void RemoveItem(ItemData item)
    {
        if (!Items.Contains(item)) return;

        // TODO add notification
        Items.Remove(item);
    }
}