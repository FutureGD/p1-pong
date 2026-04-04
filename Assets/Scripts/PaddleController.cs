using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput _input;
    [SerializeField] private float speed = 8;
    [SerializeField] private float topBound = 4f;
    [SerializeField] private float bottomBound = -4f;
    [SerializeField] private float _moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _input = new PlayerInput();
    }

    void OnEnable()
    {
        _input.Paddle.Enable();

        _input.Paddle.Move.started += OnMoveStarted;
        _input.Paddle.Move.canceled += OnMoveCancled;
    }

    void OnDisable()
    {
        _input.Paddle.Move.started -= OnMoveStarted;
        _input.Paddle.Move.canceled -= OnMoveCancled;

        _input.Paddle.Disable();
    }

    void OnMoveStarted(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<float>();
        // Debug.Log("Move input: " + _moveInput);
    }

    void OnMoveCancled(InputAction.CallbackContext ctx)
    {
        _moveInput = 0;
    }

    void FixedUpdate()
    {
        float newY = Mathf.Clamp(
            rb.position.y + _moveInput * speed * Time.fixedDeltaTime,
            bottomBound,
            topBound
        );

        rb.MovePosition(new Vector2(rb.position.x, newY));
    }
}
