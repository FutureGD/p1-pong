using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 8;
    [SerializeField] private float topBound = 4f;
    [SerializeField] private float bottomBound = -4f;
    // [SerializeField] private float _moveInput = 0;
    [SerializeField] private Transform ball;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float newY = Mathf.Clamp(Mathf.MoveTowards(transform.position.y, ball.position.y, speed * Time.fixedDeltaTime), bottomBound, topBound);

        rb.MovePosition(new Vector2(rb.position.x, newY));
    }
}
