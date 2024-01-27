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

    Vector3 _scale;
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _renderer = Animator.GetComponent<SpriteRenderer>();
        _scale = _renderer.transform.localScale;
    }

    void Update()
    {
        Animator.SetBool("isWalking", _movement.InputVelocity.magnitude > 0);

        if (_movement.InputVelocity.x != 0)
        {
            var flipped = _scale * (_movement.InputVelocity.x < 1 ? -1 : 1);

            _renderer.transform.localScale = new Vector3(flipped.x, _scale.y, _scale.z);
        }
    }
}
