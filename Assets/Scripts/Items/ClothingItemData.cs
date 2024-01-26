// author: @zsfer
using UnityEngine;

[CreateAssetMenu(fileName = "Clothing Item", menuName = "Clothing Item", order = 0)]
public class ClothingItemData : ScriptableObject
{
    public string ItemName;
    public HumorStats Stats;
    public Animator AnimationController;
}