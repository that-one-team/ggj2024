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

    public Action<bool> OnInteract;

    public void Interact(bool state)
    {
        IsInteracting = state;
        OnInteract(state);
    }

}