using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public Vector3 InputVelocity { get; private set; }
    CharacterController _cc;


    void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (PlayerInteraction.Instance.IsInteracting) return;
        InputVelocity = (transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical")).normalized * MoveSpeed;
        _cc.SimpleMove(InputVelocity);
    }
}
