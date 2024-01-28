using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DialogueInteractable : Interactable
{
    AudioSource _source;
    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        if (GetComponent<NPCBehavior>().HasInteractedAlready)
        {
            GetComponent<NPCMood>().ShowMood(0);
            PlayerInteraction.Instance.Interact(false, null);
        }
        else
        {
            PlayerDialogue.Instance.StartConversation(gameObject);
            _source.Play();
        }
    }
}