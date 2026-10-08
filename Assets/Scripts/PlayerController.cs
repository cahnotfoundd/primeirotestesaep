using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private InputAction moveAction;
    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
    }

    private void OnEnable()  { moveAction.Enable(); }
    private void OnDisable() { moveAction.Disable(); }

    private void Update()
    {
        moveDirection = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}