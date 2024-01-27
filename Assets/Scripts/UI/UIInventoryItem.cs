
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public ItemData Data { get; set; }

    public bool IsInteractable = false;
    public delegate void InteractAction();
    public InteractAction OnInteract;

    public NPCBehavior Target { get; set; }

    [SerializeField] private GameObject _tooltip;
    [SerializeField] private TextMeshProUGUI _tooltipText;

    void OnValidate()
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        if (Data == null) return;

        name = Data.Name;
        GetComponent<Image>().sprite = Data.Sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltip.SetActive(true);
        _tooltipText.text = Data.Name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltip.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsInteractable) return;

        StartCoroutine(Interact());
    }
    IEnumerator Interact()
    {
        if (!Data.HideInHud)
            PlayerInteraction.Instance.HandObject.sprite = Data.Sprite;
        var animator = PlayerAnimation.Instance.Animator;
        animator.SetTrigger("Giving");
        yield return new WaitForSeconds(1.1f);
        PlayerInteraction.Instance.HandObject.sprite = null;
        PlayerInventory.Instance.UseItem(Data, Target);
        OnInteract();
    }
}