using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    PlayerMovement _movement;

    [field: SerializeField]
    public Animator Animator { get; private set; }
    SpriteRenderer _renderer;
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _renderer = Animator.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Animator.SetBool("isWalking", _movement.InputVelocity.magnitude > 0);

        if (_movement.InputVelocity.x != 0)
        {
            _renderer.flipX = _movement.InputVelocity.x < 1;
        }
    }
}
