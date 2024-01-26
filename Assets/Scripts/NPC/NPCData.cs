using UnityEngine;

[CreateAssetMenu(fileName = "NPC Data", menuName = "NPC Data", order = 0)]
public class NPCData : ScriptableObject
{
    public string Name;
    public HumorStats Stats;
    public ClothingItemData[] Clothes;
}