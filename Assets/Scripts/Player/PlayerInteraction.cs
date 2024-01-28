using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool IsInteracting { get; private set; }
    public static PlayerInteraction Instance { get; private set; }

    [field: SerializeField]
    public SpriteRenderer HandObject { get; private set; }

    private PlayerSocialBattery _socialBattery;

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

    private void Start()
    {
        _socialBattery = GetComponent<PlayerSocialBattery>();
    }

    public static event Action<GameObject> OnInteract;

    private GameObject _target;

    public void Interact(bool state, GameObject target)
    {
        IsInteracting = state;
        GetComponent<PlayerMovement>().InputVelocity = Vector3.zero;
        _target = null;

        if (target == null) return;
        OnInteract?.Invoke(target);
        target.GetComponent<Interactable>().Interact();

        if (target.GetComponent<NPCBehavior>() && !target.GetComponent<NPCBehavior>().HasInteractedAlready)
            _socialBattery.SocialBattery -= UnityEngine.Random.Range(5, 10);
    }

    void Update()
    {
        if (_target == null) return;

        if (Input.GetKeyDown(KeyCode.E) && !IsInteracting)
            Interact(true, _target);


        if (IsInteracting)
        {
            _socialBattery.SocialBattery -= Time.deltaTime * _socialBattery.ActiveDrainMult;
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