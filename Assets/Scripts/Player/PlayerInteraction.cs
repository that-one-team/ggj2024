using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool IsInteracting { get; private set; }
    public static PlayerInteraction Instance { get; private set; }

    [field: SerializeField]
    public SpriteRenderer HandObject { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
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

        if (target.GetComponent<NPCBehavior>() && !target.GetComponent<NPCBehavior>().HasInteractedAlready)
            GetComponent<PlayerSocialBattery>().SocialBattery -= UnityEngine.Random.Range(5, 10);
    }

    void Update()
    {
        if (_target == null) return;

        if (Input.GetKeyDown(KeyCode.E) && !IsInteracting)
            Interact(true, _target);


        if (IsInteracting)
        {
            GetComponent<PlayerSocialBattery>().SocialBattery -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
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