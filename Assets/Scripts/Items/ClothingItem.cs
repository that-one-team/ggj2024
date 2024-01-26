using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class ClothingItem : MonoBehaviour
{
    [field: SerializeField] public ClothingItemData Data { get; set; }

    private void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();
        name = Data.ItemName;

        if (Data.Sprite != null)
            renderer.sprite = Data.Sprite;
    }
}