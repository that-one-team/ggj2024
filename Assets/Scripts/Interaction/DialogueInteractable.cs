using UnityEngine;

public class DialogueInteractable : Interactable
{
    public override void Interact()
    {
        if (GetComponent<NPCBehavior>().HasInteractedAlready)
        {
            GetComponent<NPCMood>().ShowMood(0);
            PlayerInteraction.Instance.Interact(false, null);
        }
        else
            PlayerDialogue.Instance.StartConversation(gameObject);
    }
}