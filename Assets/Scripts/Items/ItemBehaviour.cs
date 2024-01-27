using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider))]
public class ItemBehaviour : Interactable
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
        GetComponent<BoxCollider>().size = Vector3.one * 2;
    }

    public override void Interact()
    {
        if (PlayerInventory.Instance.AddItem(Data))
            Destroy(gameObject);

        base.Interact();
    }

}