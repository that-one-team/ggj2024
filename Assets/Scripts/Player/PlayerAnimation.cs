using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerMovement _movement;
    [SerializeField] Animator _animator;
    SpriteRenderer _renderer;
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _renderer = _animator.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _animator.SetBool("isWalking", _movement.InputVelocity.magnitude > 0);

        if (_movement.InputVelocity.x != 0)
        {
            _renderer.flipX = _movement.InputVelocity.x < 1;
        }
    }
}
