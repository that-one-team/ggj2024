using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SpriteRenderer))]
public class ClothingItemBehaviour : MonoBehaviour
{
    [field: SerializeField] public ClothingItemData Data { get; set; }

    bool _hasSpawned;

    Animator _anim;
    NPCMovement _brain;
    NavMeshAgent _agent;

    private void Start()
    {
        UpdateVisuals();

        _brain = GetComponentInParent<NPCMovement>();
        _agent = _brain.GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
    }

    public void UpdateVisuals()
    {
        var renderer = GetComponent<SpriteRenderer>();
        name = Data.ItemName;

        if (Data.Sprite != null && Data.Animation == null)
            renderer.sprite = Data.Sprite;

        if (Data.Animation != null && !_hasSpawned)
        {
            Instantiate(Data.Animation, transform.position, Quaternion.identity, transform);
            _hasSpawned = true;
        }

        transform.localScale = Vector3.one;
    }

    private void Update()
    {
        if (_anim == null) return;

        _anim.SetBool("Moving", !_agent.isStopped);
    }
}