using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider))]
public class ItemBehaviour : MonoBehaviour
{
    [field: SerializeField]
    public ItemData Data { get; set; }

    public void UpdateVisuals(ItemData data)
    {
        Data = data;
        var renderer = GetComponent<SpriteRenderer>();

        Data.name = Data.Name;
        name = Data.Name;

        if (Data.Sprite != null)
            renderer.sprite = Data.Sprite;
    }

    private void OnValidate()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
}