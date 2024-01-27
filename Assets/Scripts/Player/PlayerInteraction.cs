using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool IsInteracting { get; private set; }
    public static PlayerInteraction Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public static event Action<GameObject> OnInteract;

    private GameObject _target;

    public void Interact(bool state, GameObject target)
    {
        IsInteracting = state;
        _target = null;

        if (target == null) return;
        OnInteract?.Invoke(target);
        target.GetComponent<Interactable>().Interact();
    }

    void Update()
    {
        if (_target == null) return;

        if (Input.GetKeyDown(KeyCode.E) && !IsInteracting)
            Interact(true, _target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            other.GetComponent<Interactable>().CanInteract = true;
            _target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            other.GetComponent<Interactable>().CanInteract = false;
            _target = null;
        }
    }
}