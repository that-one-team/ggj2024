using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerMovement _movement;
    Animator _animator;
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }
}
