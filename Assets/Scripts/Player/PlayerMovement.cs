using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public Vector3 InputVelocity { get; set; }
    CharacterController _cc;

    bool _isFrozen;

    private void OnEnable()
    {
        GameManager.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= GameOver;
    }

    void GameOver(OverReasons reason)
    {
        _isFrozen = true;
    }


    void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (PlayerInteraction.Instance.IsInteracting || _isFrozen) return;
        InputVelocity = (transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical")).normalized * MoveSpeed;
        _cc.SimpleMove(InputVelocity);
    }
}
