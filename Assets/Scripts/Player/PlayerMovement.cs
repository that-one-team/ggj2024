using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    Vector3 _vel;
    CharacterController _cc;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        _vel = (transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical")).normalized * MoveSpeed;
        _cc.SimpleMove(_vel);
    }
}
