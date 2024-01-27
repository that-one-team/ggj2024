using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool CanInteract;
    [field: SerializeField]
    public GameObject IndicatorUI { get; set; }

    private void OnValidate()
    {
        UpdateIndicator();
    }

    protected void UpdateIndicator()
    {
        if (IndicatorUI == null && transform.childCount > 0)
        {
            IndicatorUI = transform.GetChild(0).gameObject;
        }

    }

    private void Update()
    {
        if (PlayerInteraction.Instance.IsInteracting)
        {
            IndicatorUI.SetActive(false);
            return;
        }

        IndicatorUI.SetActive(CanInteract);
    }

    public virtual void Interact()
    {
        PlayerInteraction.Instance.Interact(false, null);
    }
}