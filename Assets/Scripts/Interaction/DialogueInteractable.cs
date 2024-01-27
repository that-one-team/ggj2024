using UnityEngine;

public class DialogueInteractable : Interactable
{
    public override void Interact()
    {
        PlayerDialogue.Instance.StartConversation(gameObject);
    }
}