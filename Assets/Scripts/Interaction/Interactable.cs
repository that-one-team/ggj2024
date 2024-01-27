using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool CanInteract;
    [SerializeField] GameObject _indicatorUI;

    private void OnValidate()
    {
        if (_indicatorUI == null && transform.childCount > 0)
        {
            _indicatorUI = transform.GetChild(0).gameObject;
        }
    }

    private void Update()
    {
        if (PlayerInteraction.Instance.IsInteracting)
        {
            _indicatorUI.SetActive(false);
            return;
        }

        _indicatorUI.SetActive(CanInteract);
    }

    public virtual void Interact()
    {
        PlayerInteraction.Instance.Interact(false, null);
    }
}