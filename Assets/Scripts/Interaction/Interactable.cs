using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool CanInteract;
    [SerializeField] GameObject _indicatorUI;

    private void Update()
    {
        if (PlayerInteraction.Instance.IsInteracting)
        {
            _indicatorUI.SetActive(false);
            return;
        }

        _indicatorUI.SetActive(CanInteract);
    }

    public virtual void Interact() { }
}