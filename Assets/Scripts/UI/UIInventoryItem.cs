
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

    [SerializeField] private GameObject _tooltip;
    [SerializeField] private TextMeshProUGUI _tooltipText;

    private void OnValidate()
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
        OnInteract();
    }
}